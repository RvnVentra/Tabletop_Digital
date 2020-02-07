import React, { Component } from 'react';
import './Table.css';

const signalR = require('@aspnet/signalr');

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
        this.state =
        {
            connection: null,
            topCard: null,
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

                this.state.connection.on('UpdateTable', (table) =>
                {
                    this.setState({ topCard: table.topCard, loading: false });
                });

                this.state.connection.invoke("UpdateTable");
            })            
            .catch(err => console.log('Error while establishing connection :('));
        });
    }

    render()
    {
        let topCard;

        if (!this.state.loading)
        {
            topCard = <img src={cardImgs[this.state.topCard.color][this.state.topCard.number]} alt="card" />
        }

        return (
            <div>
                <h3>{statusText}</h3>
                {topCard}
                
                <br />
                <Hand />
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
    }

    drawCard()
    {
        this.state.connection.invoke("DrawCard");
    }

    cardClick(i)
    {
        console.log(i);
        this.state.connection.invoke("PlayCard", i);
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

                cards.push(<Card key={i} connection={this.state.connection} id={i} img={cardImgs[color][number]} />);

                //cards.push(RenderCard(
                //    {
                //        onClick: (e) => this.cardClick(i, e),
                //        img: cardImgs[color][number]
                //    }));

                //cards.push(
                //    <button className="hand" key={i} onClick={(e) => this.cardClick(i, e)}>
                //        <img src={cardImgs[color][number]} alt="card" />
                //    </button>);
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

function RenderCard(props)
{
    return (
        <button className="hand" onClick={props.onClick}>
            <img src={props.img} alt="card" />
        </button>
    );
}

class Card extends Component
{
    constructor(props)
    {
        super(props);
        this.state =
        {
            connection: this.props.connection,
            id: this.props.id,
            img: this.props.img
        };
    }

    cardClick(i)
    {
        console.log(i);
        this.state.connection.invoke("PlayCard", i);
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