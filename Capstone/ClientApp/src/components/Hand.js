import React, { Component } from 'react';
import './Hand.css';

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

export class Hand extends Component
{
    constructor(props)
    {
        super(props);
        this.state =
        {
            connection: null,
            cards: [],
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

                this.state.connection.on('UpdateHand', (hand) =>
                {
                    this.setState({ cards: hand, loading: false });
                    console.log(hand);
                });

                this.state.connection.invoke("UpdateHand");
            })
                .catch(err => console.log('Error while establishing connection :('));
        });

        //Preload card image files
        for (let i = 0; i < 4; i++)
        {
            for (let n = 0; n < 10; n++)
            {
                cardImgs[i][n] = new Image();
                cardImgs[i][n] = "images/cards/" + COLORS[i] + "_" + n + ".png";
            }
        }
    }

    drawCard()
    {
        this.state.connection.invoke("DrawCard");
    }

    cardClick(i)
    {
        //console.log("cardClick" + i);
        this.state.connection.invoke("PlayCard", i);
    }

    renderCard(props)
    {
        return (
            <button key={props.key} className="hand" onClick={(e) => this.cardClick(props.key, e)}>
                <img src={props.img} alt="card" />
            </button>
        );
    }

    render()
    {
        let cards = [];

        if (!this.state.loading)
        {
            for (var i = 0; i < this.state.cards.length; i++)
            {
                var number = this.state.cards[i].number;
                var color = this.state.cards[i].color;

                cards.push(this.renderCard(
                    {
                        key: i,
                        img: cardImgs[color][number]
                    }));
            }
        }

        return (
            <div>
                <button id="draw-card" onClick={(e) => this.drawCard(e)}>Draw Card</button>
                <br />
                {cards}
            </div>
        );
    }
}