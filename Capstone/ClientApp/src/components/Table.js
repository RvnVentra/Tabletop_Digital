import React, { Component } from 'react';

var cardImgs;

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

        cardImgs = this.props.cardImgs;
    }

    componentDidMount()
    {
        this.state.connection.on('UpdateTable', (table) =>
        {
            this.setState(
                {
                    topCard: table.topCard,
                    loading: false
                });
        });

        this.state.connection.invoke('UpdateTable');
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