import React, { Component } from 'react';

export class CreateGame extends Component
{
    static displayName = CreateGame.name;

    constructor(props)
    {
        super(props);
        this.state = { code: null };
    }

    componentDidMount()
    {
        fetch('Main/CreateGame')
            .then(response => response.text())
            .then(code =>
            {
                console.log(code);
                this.setState({ code });
            });
    }

    render()
    {

        return (
            <div>
                <h1>Create Game</h1>
                <br />
                {this.state.code}
            </div>
        );
    }
}
