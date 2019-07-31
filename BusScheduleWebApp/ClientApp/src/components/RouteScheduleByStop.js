import React, { Component } from 'react';
import StopRouteInfoTableDisplay from './StopRouteInfoTableDisplay';


export class RouteScheduleByStop extends Component {

    constructor(props) {
        super(props);
        this.state = {
            stops: [],
            loading: true,
            selectedStopNumber: "1"
        };
    }

    componentDidMount() {
        this.getBusStops();
        this.getFullSchedule();
    }

    render() {
        const { stops, selectedStopNumber, fullSchedule } = this.state;
        let optionItems = stops.map((stop) =>
            <option key={stop.busStop} value={stop.busStopNumber}>{stop.busStop}</option>
        );
        let dispText = "Routes for Stop" + selectedStopNumber;
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <StopRouteInfoTableDisplay
                fullSchedule={ fullSchedule }
                text={dispText} />

        return (
            <div>
                Select A Stop:&nbsp;
                <select value={this.state.selectedStopN} onChange={this.handleChange}>
                    {optionItems}
                </select>

                {contents}
            </div>
        )
    }

    handleChange = (event) => {
        this.setState({ selectedStopNumber: event.target.value }, () => {
            this.getFullSchedule();
        }); 
        
    }

    async getBusStops() {
        const response = await fetch('http://localhost:62673/api/Stops');
        const data = await response.json();
        this.setState({ stops: data });
    }

    async getFullSchedule() {
        let url = "http://localhost:62673/api/Buses/" + this.state.selectedStopNumber + "/time/";
        let response = await fetch(url);
        let data =  await response.json();
        this.setState({ fullSchedule: data, loading: false });
    }
}

