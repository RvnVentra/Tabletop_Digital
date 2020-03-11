import React, { Component } from 'react';

export class Home extends Component
{
    static displayName = Home.name;

	postTest()
	{
        var req = new XMLHttpRequest();

        req.addEventListener('load', () =>
        {
            console.log(req.responseText)
     
        });

        req.open('Post', 'Main/AddAccount');
        req.send(JSON.stringify({ username: 'data' }));
	}

    render()
	{
        this.postTest();

        return(
            <div id="home">
                <h1>Home</h1>
                <a className="home-button" href="/createGame">Create Game</a>
                <a className="home-button" href="/game">Join Game</a>
            </div>
        );
    }
}
