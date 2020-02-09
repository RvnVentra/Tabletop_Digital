import React, { Component } from 'react';

export class PlayerList extends Component
{
    constructor(props)
    {
        super(props);
        this.state =
        {
            players: [],
            connection: this.props.connection
        };
    }

    componentDidMount()
    {
        this.state.connection.on('UpdatePlayerList', (data) =>
        {
            //console.log(data);
            this.setState({ players: data });
        });

        this.state.connection.invoke('UpdatePlayerList');
    };

    render()
    {
        let players = [];

        for (var i = 0; i < this.state.players.length; i++)
        {
            players.push(
                <tr key={i}>
                    <td>{this.state.players[i].name}</td>
                    <td>{this.state.players[i].cardCount}</td>
                </tr>);
        }

        return (
            <table id="player-list">
            
                <thead>
                <tr>
                    <th>Players</th>
                    <th>Cards</th>
                    </tr>
                </thead>

                <tbody>
                    {players}
                </tbody>
            
            </table>);
    }
}
