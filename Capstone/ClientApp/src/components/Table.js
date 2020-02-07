import React, { Component } from 'react';
import './Table.css';

const signalR = require('@aspnet/signalr');


//connection.on("Receive", data =>
//{
//    console.log(data);
//});

//connection.start()
//    .then(() => connection.invoke("Send", "Hello"));

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

//Preload card image files
for (let i = 0; i < 4; i++)
{
    for (let n = 0; n < 10; n++)
    {
        cardImgs[i][n] = new Image();
        cardImgs[i][n] = "images/cards/" + COLORS[i] + "_" + n + ".png";
    }
}

export class Table extends Component
{
    static displayName = Table.name;

    constructor(props)
    {
        super(props);
        this.state = { connection: null, topCard: null, loading: true };
    }

    componentDidMount()
    {
        this.setState({ connection: new signalR.HubConnectionBuilder().withUrl("/gameHub").build() }, () =>
        {
            this.state.connection
                .start()
                .then(() =>
                {
                    console.log('Connection started!');

                    this.state.connection.on('UpdateTable', (table) =>
                    {
                        this.setState({ topCard: table.topCard, loading: false });
                        console.log("TEST");
                    });

                    this.state.connection.invoke("UpdateTable");
                })            
                .catch(err => console.log('Error while establishing connection :('));
        });
    }

    drawCard()
    {
        this.state.connection.invoke("Send", "Draw!");
    }


    render()
    {
        //var hand;
        let topCard;

        if (!this.state.loading)
        {
            topCard = <img src={cardImgs[this.state.topCard.color][this.state.topCard.number]} alt="card" />
            //topCard = <img src={"images/cards/" + COLORS[this.state.topCard.color] + "_"
            //    + this.state.topCard.number + ".png"} alt="card" />

            //hand = [];

            //for (var i = 0; i < this.state.hand.length; i++)
            //{
            //    var number = this.state.hand[i].number;
            //    var color = this.state.hand[i].color;

            //    hand.push(<Card key={i} id={i} img={cardImgs[color][number]} />);
            //}
        }

        return (
            <div>
                <h3>{statusText}</h3>
                {topCard}
                <button id="draw-card" onClick={(e) => this.drawCard(e)}>Draw Card</button>
                <br />
                
            </div>
        );
    }
}

class Hand extends Component
{
    constructor(props)
    {
        super(props);
        this.state =
        {
            id: this.props.id,
            img: this.props.img
        };
    }
}

class Card extends Component
{
    constructor(props)
    {
        super(props);
        this.state =
        {
            id: this.props.id,
            img: this.props.img
        };
    }

    cardClick(i)
    {
        console.log(i);


        fetch('api/Game/PlayCard?cardID=' + i,
            {
                method: "POST"
                //headers: { 'Content-Type': 'application/json' },
                //cardID: '7'
            })
            .then(() =>
            {
                statusText = i + " has been clicked";
            });
    }

    render()
    {
        return (
            <button className="hand" onClick={(e) => this.cardClick(this.state.id, e)}>
                <img src={this.state.img} alt="card" />
            </button>
        );
    }
}