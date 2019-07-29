import React, { Component } from 'react';
import StopRouteInfoTableDisplay from './StopRouteInfoTableDisplay';


export class FullSchedule extends Component {
    
    constructor(props) {
        super(props);
        this.state = { fullSchedule: [], loading: true };
    }

    componentDidMount() {
        this.getFullSchedule();
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <StopRouteInfoTableDisplay
                fullSchedule={this.state.fullSchedule}
                text="All stops and routes schedule" />

        return (
            <div>
                {contents}
            </div>
        );
    }
    
    async getFullSchedule() {
        const response = await fetch('/api/Buses');
        const data = await response.json();
        this.setState({ fullSchedule: data, loading: false });
    }
}

