import React, { Component } from 'react';
import { WebSocketHttp } from './WebSocketHttp';
import { WebSocketHTTPClass } from '../custom/WebSocketHTTPClass';

export class RouteScheduleByStop extends Component {

    constructor(props) {
        super(props);
        const stopsApiUrl = "http://localhost:62673/api/BusStops";
        const wsApiUrl = "http://localhost:62673/api/BusStopSocket/";
        const initApiUrl = "http://localhost:62673/api/Buses/1/time";
        const wsUrl = "ws://localhost:62673/wsStop";
        const webSocket = new WebSocketHTTPClass(wsUrl);
        this.state = {
            socket: webSocket.Socket,
            isSocketOpen: true,
            fullSchedule: [],
            stops: [],
            selectedStopNumber: "1",
            wsUrl: wsUrl,
            stopsApiUrl: stopsApiUrl,
            wsApiUrl: wsApiUrl,
            initApiUrl: initApiUrl,
            loading: true
        };
    }

    componentDidMount() {
        this.getBusStops();
        this.getFullSchedule();
    }

    handleChange = (event) => {
        this.setState({ selectedStopNumber: event.target.value }, () => {
            const { wsUrl } = this.state;
            const webSocket = new WebSocketHTTPClass(wsUrl);
            this.setState({ socket: webSocket.Socket });
            this.setState({ loading: true });
            this.getFullSchedule();
            this.refWebSocketHttp.stopRouteDataByTime();
            console.log("handleChange");
        });
    }

    async getBusStops() {
        const { stopsApiUrl } = this.state;
        const response = await fetch(stopsApiUrl);
        const data = await response.json();
        this.setState({ stops: data });
    }

    async getFullSchedule() {
        const { initApiUrl } = this.state;
        let response = await fetch(initApiUrl);
        let data = await response.json();
        this.setState({ fullSchedule: data, loading: false }, () => {
            console.log(this.state.fullSchedule);
        });
    }

    render() {
        const { stops, selectedStopNumber, wsUrl, wsApiUrl, socket, isSocketOpen, fullSchedule } = this.state;
        let optionItems = stops.map((stop) =>
            <option key={stop.busStop} value={stop.busStopNumber}>{stop.busStop}</option>
        );
        let dispText = "Routes By Stop"
        let apiUrl = wsApiUrl + selectedStopNumber;
        let busData = this.state.loading
            ? <p><em>Loading...</em></p>
            : <WebSocketHttp
                fullSchedule = { fullSchedule }
                socket = { socket }
                isSocketOpen = { isSocketOpen }
                apiUrl = { apiUrl }
                wsUrl={wsUrl}
                dispText={dispText}
                ref={WebSocketHttp => { this.refWebSocketHttp = WebSocketHttp }}/>
        return (
            <div>
                Select A Stop:&nbsp;
                <select value={ selectedStopNumber } onChange={this.handleChange}>
                    {optionItems}
                </select>
                <br />
                {busData}
            </div>
        )
    }

    componentWillUnmount() {
        this.state.socket.close();
        console.log("unmount");
    }
}

