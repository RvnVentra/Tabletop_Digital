import React, { Component } from 'react';

export class Home extends Component
{
    static displayName = Home.name;

    constructor(props)
    {
        super(props);
        this.state = { text: "", loading: true };

        var PL = {
            a: 1,
            b: 2
        };

        var data = new FormData();
        data.append("PoLos", JSON.stringify(PL));

        fetch('main/test',
            {
                method: "POST",
                body: data
            }).
            then(response => response.text())
            .then(data =>
            {
                console.log(data);

                this.setState({ text: data, loading: false });
            });

    }

    render()
	{
        return(
            <div id="home">
                <h1>Home</h1>
                <a className="home-button" href="/createGame">Create Game</a>
                <a className="home-button" href="/game">Join Game</a>
            </div>
        );
    }
}
