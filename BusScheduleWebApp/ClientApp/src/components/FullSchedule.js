import React, { Component } from 'react';
import StopRouteInfoTableDisplay from './StopRouteInfoTableDisplay';


export class FullSchedule extends Component {
    
    constructor(props) {
        super(props);
        const apiUrl = "http://localhost:62673/api/Buses";
        this.state = { fullSchedule: [], loading: true, apiUrl: apiUrl };
    }

    componentDidMount() {
        this.getFullSchedule();
    }

    render() {
        const { fullSchedule, loading } = this.state;
        let contents = loading
            ? <p><em>Loading...</em></p>
            : <StopRouteInfoTableDisplay
                fullSchedule={fullSchedule}
                text="All stops and routes schedule" />

        return (
            <div>
                {contents}
            </div>
        );
    }
    
    async getFullSchedule() {
        const { apiUrl } = this.state;
        const response = await fetch(apiUrl);
        const data = await response.json();
        this.setState({ fullSchedule: data, loading: false });
    }
}