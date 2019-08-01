import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { RouteScheduleByStop } from './components/RouteScheduleByStop';
import { RoutesByCurrentTime } from './components/RoutesByCurrentTime';
import { FullSchedule } from './components/FullSchedule';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;
  
    render() {
        return (
            <Layout>
                <Route exact path='/' component={RouteScheduleByStop} />
                <Route path='/fetch-data' component={RoutesByCurrentTime} />
                <Route path='/full-schedule' component={FullSchedule} />
            </Layout>
        );
    }
}
