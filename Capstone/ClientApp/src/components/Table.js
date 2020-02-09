import React, { Component } from 'react';

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
            connection: this.props.connection,
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

        this.state.connection.on('UpdateTable', (table) =>
        {
            this.setState({ topCard: table.topCard, loading: false });
        });

        this.state.connection.invoke("UpdateTable");
    }

    render()
    {  
        if (this.state.loading)
            return false;

        return (
            <div>   
                <img src={cardImgs[this.state.topCard.color][this.state.topCard.number]} alt="card" />
            </div>
        ); 
    }
}

