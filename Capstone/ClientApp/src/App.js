import React, { Component } from 'react';
import { BrowserRouter as Router, Switch, Route, Link, useParams } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home'; 
import { Game } from './components/Game';
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
                <Route exact path='/game' component={Game} />
                <Route exact path='/createGame' component={CreateGame} />
                <Route path='/debug' component={DebugLog} />
            </Layout>
        );
    }
}

    //<Route path='/table' component={Table} />
    //<Route path='/Hand' component={Hand} />