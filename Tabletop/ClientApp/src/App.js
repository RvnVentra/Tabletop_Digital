import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home'; 
import { JoinGame } from './components/JoinGame';
import { CreateGame } from './components/CreateGame';
import { DebugLog } from './components/DebugLog';

export default class App extends Component
{
    static displayName = App.name;

    render()
    {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/game' component={JoinGame} />
                <Route exact path='/createGame' component={CreateGame} />
                <Route path='/debug' component={DebugLog} />
            </Layout>
        );
    }
}

    //<Route path='/table' component={Table} />
    //<Route path='/Hand' component={Hand} />