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
        let stops = this.state.stops;
        //debugger;
        let optionItems = stops.map((stop) =>
            <option key={stop.busStop} value={stop.busStopNumber}>{stop.busStop}</option>
        );

        let dispText = "Routes for Stop" + this.state.selectedStopNumber;
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <StopRouteInfoTableDisplay
                fullSchedule={this.state.fullSchedule}
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
        const response = await fetch('/api/Stops');
        const data = await response.json();
        //debugger;
        this.setState({ stops: data });
    }

    async getFullSchedule() {
        let url = "/api/Buses/" + this.state.selectedStopNumber + "/time/";
        //debugger;
        let response = await fetch(url);
        let data =  await response.json();
        //debugger;
        this.setState({ fullSchedule: data, loading: false });
    }
}

