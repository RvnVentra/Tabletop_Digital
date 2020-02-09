import React, { Component } from 'react';
import { Hand } from './Hand';
import { ChatBox } from './ChatBox';
import { PlayerList } from './PlayerList';

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
        //Preload card image files
        for (let i = 0; i < 4; i++)
        {
            for (let n = 0; n < 10; n++)
            {
                cardImgs[i][n] = new Image();
                cardImgs[i][n] = "images/cards/" + COLORS[i] + "_" + n + ".png";
            }
        }

        this.setState({ connection: new signalR.HubConnectionBuilder().withUrl("/gameHub").build() }, () =>
        {
            this.state.connection.start().then(() =>
            {
                console.log('Connection started!');
                //console.log(this.state.connection.hub.id);

                this.state.connection.on('UpdateTable', (table) =>
                {
                    this.setState({ topCard: table.topCard, loading: false });
                });

                this.state.connection.invoke("UpdateTable");
            })            
            .catch(err => console.log('Error while establishing connection :('));
        });
    }

    render()
    {  
        if (this.state.loading)
            return (<div><h1> LOADING </h1></div>);
        
        return (
            <div>
                <div className="left">
                    <img src={cardImgs[this.state.topCard.color][this.state.topCard.number]} alt="card" />
                    <br />
                    <Hand connection={this.state.connection} />
                </div>

                <div className="right">
                    <PlayerList connection={this.state.connection} />
                    <ChatBox connection={this.state.connection} />
                </div>
            </div>
        ); 
    }
}

