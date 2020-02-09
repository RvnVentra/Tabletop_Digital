import React, { Component } from 'react';
import { Table } from './Table'
import { Hand } from './Hand';
import { ChatBox } from './ChatBox';
import { PlayerList } from './PlayerList';
import { NameEntry } from './NameEntry';

const signalR = require('@aspnet/signalr');

export class Game extends Component
{
    static displayName = Table.name;

    constructor(props)
    {
        super(props);
        this.state =
        {
            connection: null,
            loggedIn: false,
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
                this.setState({ loading: false });

                this.state.connection.on('JoinGame', () =>
                {
                    this.setState({ loggedIn: true });
                });

            })
            .catch(err => console.log('Error while establishing connection :('));
        });
    }

    render()
    {
        if (this.state.loading)
            return (<div><h1> LOADING </h1></div>);

        if (!this.state.loggedIn)
            return (
                <div>
                    <NameEntry connection={this.state.connection} />
                </div>
            );

        return (
            <div>
                <div className="left">
                    <Table connection={this.state.connection} />
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

