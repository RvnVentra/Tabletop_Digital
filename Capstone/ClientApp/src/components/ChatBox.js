import React, { Component } from 'react';

export class ChatBox extends Component
{
    constructor(props)
    {
        super(props);
        this.state =
        {
            input: '',
            chatBox: [],
            connection: this.props.connection
        };
    }

    componentDidMount()
    {
        this.state.connection.on('UpdateChat', (data) =>
        {
            //console.log(data);
            this.setState({ chatBox: data });
        }); 

        this.state.connection.invoke('UpdateChat');
    };

    sendMessage = () =>
    {
        this.state.connection
            .invoke('Chat', this.state.input)
            .catch(err => console.error(err));

        this.setState({ input: '' });
    };

    render()
    {
        let chatlog = [];

        for (let i = this.state.chatBox.length - 1; i >= 0; i--)
        {
            chatlog.push(
                <div key={i} className="chatMessage">
                    <h3>{this.state.chatBox[i].author}</h3>
                    <p>{this.state.chatBox[i].value}</p>
                </div>);
        }

        return (
            <div id="chat-box">
                <br />
                <input id= "chatInput"
                    type="text"
                    value={this.state.input}
                    onChange={e => this.setState({ input: e.target.value })}
                />
                <br/><br/>
                <button onClick={this.sendMessage}>Send</button>

                <br/><br/>
                <div>
                    {chatlog}
                </div>
            </div>
        );
    }
}
