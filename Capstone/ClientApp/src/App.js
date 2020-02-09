import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Table } from './components/Table';
import { Hand } from './components/Hand';
import { DebugLog } from './components/DebugLog';

export default class App extends Component
{
    static displayName = App.name;

    render()
    {
        return (
            <Layout>
                <Route exact path='/' component={Table} />
                <Route path='/debug' component={DebugLog} />
            </Layout>
        );
    }
}

    //<Route path='/table' component={Table} />
    //<Route path='/Hand' component={Hand} />