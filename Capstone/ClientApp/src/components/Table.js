import React, { Component } from 'react';
import { Hand } from './Hand';
import { ChatBox } from './ChatBox';
import { DebugLog } from './DebugLog';
import './Table.css';

const signalR = require('@aspnet/signalr');

const COLORS =
{
    0: "blue",
    1: "red",
    2: "green",
    3: "yellow"
};

var statusText = " ";
var cardImgs = new Array(4);
cardImgs[0] = [];
cardImgs[1] = [];
cardImgs[2] = [];
cardImgs[3] = [];


export class Table extends Component
{
    static displayName = Table.name;

    constructor(props)
    {
        super(props);
        this.state =
        {
            connection: null,
            topCard: null,
            loading: true
        };
    }

    componentDidMount()
    {
        this.setState({ connection: new signalR.HubConnectionBuilder().withUrl("/gameHub").build() }, () =>
        {
            this.state.connection.start().then(() =>
            {
                console.log('Connection started!');

                this.state.connection.on('UpdateTable', (table) =>
                {
                    this.setState({ topCard: table.topCard, loading: false });
                });

                this.state.connection.invoke("UpdateTable");
            })            
            .catch(err => console.log('Error while establishing connection :('));
        });

        //Preload card image files
        for (let i = 0; i < 4; i++)
        {
            for (let n = 0; n < 10; n++)
            {
                cardImgs[i][n] = new Image();
                cardImgs[i][n] = "images/cards/" + COLORS[i] + "_" + n + ".png";
            }
        }
    }

    render()
    {
        let topCard;
        let chatBox;

        if (!this.state.loading)
        {
            topCard = <img src={cardImgs[this.state.topCard.color][this.state.topCard.number]} alt="card" />;
            chatBox = <ChatBox connection={this.state.connection} />;
        }

        return (
            <div>
                <div className= "left">
                    {topCard}
                    <br/>
                    <Hand />
                </div>
                
                <div className= "right">
                    {chatBox}
                </div>
            </div>
        );
    }
}

