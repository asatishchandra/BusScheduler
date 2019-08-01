import React, { Component } from 'react';
import { WebSocketHttp } from './WebSocketHttp';
import { WebSocketHTTPClass } from '../custom/WebSocketHTTPClass';

export class RouteScheduleByStop extends Component {

    constructor(props) {
        super(props);
        const stopsApiUrl = "http://localhost:62673/api/BusStops";
        const wsApiUrl = "http://localhost:62673/api/BusStopSocket/";
        const initApiUrl = "http://localhost:62673/api/Buses/";
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

    async getBusStops() {
        const { stopsApiUrl } = this.state;
        const response = await fetch(stopsApiUrl);
        const data = await response.json();
        this.setState({ stops: data });
    }

    async getFullSchedule() {
        const { initApiUrl, selectedStopNumber } = this.state;
        let apiUrl = initApiUrl + selectedStopNumber + "/time";
        console.log(apiUrl);
        let response = await fetch(apiUrl);
        let data = await response.json();
        this.setState({ fullSchedule: data, loading: false }, () => {
            console.log(this.state.fullSchedule);
        });
    }

    handleChange = (event) => {
        const { wsUrl } = this.state;
        const webSocket = new WebSocketHTTPClass(wsUrl);
        this.setState({ selectedStopNumber: event.target.value }, () => {
            this.setState({
                socket: webSocket.Socket,
                loading: true
            });
            this.getFullSchedule();
            this.refWebSocketHttp.stopRouteData();
            console.log("handleChange");
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

