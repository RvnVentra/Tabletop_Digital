import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Table } from './components/Table';
import { Hand } from './components/Hand';

export default class App extends Component
{
    static displayName = App.name;

    render()
    {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/table' component={Table} />
                <Route path='/Hand' component={Hand} />
            </Layout>
        );
    }
}
