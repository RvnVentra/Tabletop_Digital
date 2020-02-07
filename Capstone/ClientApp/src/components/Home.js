import React, { Component } from 'react';
//const signalR = require('@aspnet/signalr');

//let connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

//connection.on("Receive", data =>
//{
//    console.log(data);
//});

//connection.start()
//    .then(() => connection.invoke("Send", "Hello"));


export class Home extends Component
{
    static displayName = Home.name;

    render()
    {
        return(
            <div>
            <h1>Hello, world!</h1>

            </div>
        );
    }
}
