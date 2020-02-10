import React, { Component } from 'react';

var cardImgs;

export class Hand extends Component
{
    constructor(props)
    {
        super(props);
        this.state =
        {
            connection: this.props.connection,
            cards: [],
            loading: true
        };

        cardImgs = this.props.cardImgs;
    }

    componentDidMount()
    {
        this.state.connection.on('UpdateHand', (hand) =>
        {
            this.setState(
                {
                    cards: hand,
                    loading: false
                });
        });

        this.state.connection.invoke("UpdateHand");
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