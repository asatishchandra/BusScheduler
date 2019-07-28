import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Test } from './components/test';

import './custom.css'
var message = "";
export default class App extends Component {
  static displayName = App.name;
  
render () {
    return (
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/counter' component={Counter} />
            <Route path='/fetch-data' component={FetchData} />
            <Route path='/test' component={Test} />
        </Layout>
    );
}
    onMessage(message) {
        debugger;
    }

    sendMessage() {
        let user = "web client"
        this.refWebSocketHttp.sendMessage(message);
    }  

}
