import React, { Component } from 'react';
import { JoinGame } from './JoinGame';
import { CreateGame } from './CreateGame';

export class Home extends Component
{
    static displayName = Home.name;

    constructor(props)
    {
        super(props);
        this.state =
        {
            gameState : null
        };
    }

    componentDidMount()
    {
        this.setState({ gameState: 'Home' });
    }

    joinGame()
    {
        this.setState({ gameState: 'JoinGame' });
    }

    createGame()
    {
        this.setState({ gameState: 'CreateGame' });
    }

    render()
    {
        switch (this.state.gameState)
        {
            default:
                return (
                    <div id="home">
                        <h1>Home</h1>
                        <a className="home-button" onClick={(e) => this.createGame(e)}>Create Game</a>
                        <a className="home-button" onClick={(e) => this.joinGame(e)}>Join Game</a>
                    </div>
                );

            case 'JoinGame':
                return (<div><JoinGame /></div>);

            case 'CreateGame':
                return (<div><CreateGame /></div>);
        }
    }
}
