import React, { Component } from 'react';
import { Table } from './Table'
import { Hand } from './Hand';
import { ChatBox } from './ChatBox';
import { PlayerList } from './PlayerList';
import { NameEntry } from './NameEntry';

const signalR = require('@aspnet/signalr');

const COLORS =
{
    0: "blue",
    1: "red",
    2: "green",
    3: "yellow"
};

var cardImgs = new Array(4);
cardImgs[0] = [];
cardImgs[1] = [];
cardImgs[2] = [];
cardImgs[3] = [];

export class Game extends Component
{
    static displayName = Table.name;

    constructor(props)
    {
        super(props);
        this.state =
        {
            connection: null,
            playerID: null,
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
                this.loadAssets();

                this.setState({ loading: false });

                this.state.connection.on('JoinGame', (playerID) =>
                {
                    this.setState({ playerID });
                });

            })
            .catch(err => console.log('Error while establishing connection :('));
        });
    }

    loadAssets()
    {
        //Preload card image files
        for (let i = 0; i < 4; i++)
        {
            for (let n = 0; n < 13; n++)
            {      
                cardImgs[i][n] = new Image();
                cardImgs[i][n] = "images/cards/" + COLORS[i] + "_" + n + ".png";
            }

            cardImgs[i][10] = "images/cards/" + COLORS[i] + "_picker" + ".png";
            cardImgs[i][11] = "images/cards/" + COLORS[i] + "_reverse" + ".png";
            cardImgs[i][12] = "images/cards/" + COLORS[i] + "_skip" + ".png";
        }
    }

    render()
    {
        if (this.state.loading)
            return (<div><h1> LOADING </h1></div>);

        if (this.state.playerID == null)
            return (
                <div>
                    <NameEntry connection={this.state.connection} />
                </div>
            );

        return (
            <div>
                <div className="left">
                    <Table connection={this.state.connection} cardImgs={cardImgs} />
                    <Hand connection={this.state.connection} cardImgs={cardImgs} />
                </div>

                <div className="right">
                    <PlayerList connection={this.state.connection} playerID={this.state.playerID} />
                    <ChatBox connection={this.state.connection} />
                </div>
            </div>
        );
    }
}

