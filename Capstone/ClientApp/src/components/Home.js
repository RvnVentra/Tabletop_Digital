import React, { Component } from 'react';

export class Home extends Component
{
    static displayName = Home.name;

    render()
    {
        return(
            <div>
                <h1>Home</h1>
                <a href="/createGame">Create Game</a>
                <a href="/game">Join Game</a>
            </div>
        );
    }
}
