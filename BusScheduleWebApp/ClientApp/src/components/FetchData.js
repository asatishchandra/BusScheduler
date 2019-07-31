import React, { Component } from 'react';
import StopRouteInfoTableDisplay from './StopRouteInfoTableDisplay';
import { WebSocketHTTP } from '../custom/WebSocketHTTP';

export class FetchData extends Component {
    constructor(props) {
        super(props);
        const apiUrl = "http://localhost:62673/api/Buses/time";
        const wsUrl = "ws://localhost:62673/ws";
        const webSocket = new WebSocketHTTP(wsUrl);
        this.state = {
            socket: webSocket.Socket,
            isSocketOpen: true,
            wsUrl: wsUrl,
            apiUrl: apiUrl,
            fullSchedule: [],
            loading: true,
            selectedStopNumber: "1"
        };
    } 

    componentDidMount() {
        this.connect();
        this.stopRouteDataByTime();
    }

    connect() {
        const { socket } = this.state;
        socket.onopen = this.onOpen;
        socket.onclose = this.onClose;
        socket.onmessage = this.onMessage;
        socket.onerror = this.onError
    }

    handleClick = () => {
        const { isSocketOpen } = this.state;
        isSocketOpen ? this.onClose() : this.onOpen();
        this.setState({ isSocketOpen: !isSocketOpen });
    }

    onClose = () => {
        const { wsUrl, socket } = this.state;
        socket.close();
        console.log("closed connection from " + wsUrl);
    }

    onOpen = () => {
        const { wsUrl, isSocketOpen } = this.state;
        if (!isSocketOpen) {
            this.setState({ isSocketOpen: true });
            this.connect();
            this.stopRouteDataByTime();
            console.log("opened connection to " + wsUrl);
        }
    }

    onMessage = (evt) => {
        this.setState({ fullSchedule: JSON.parse(evt.data) }, () => { console.log('onMessage'); });
        console.log(evt.data);
    }

    onError = (evt) => {
        console.log("error: " + evt.data);
    }

    async stopRouteDataByTime() {
        const { apiUrl } = this.state;
        await fetch(apiUrl);
        this.setState({ fullSchedule: [], loading: false });
    }

    componentWillUnmount() {
        this.onClose();
        console.log("unmount");
    }

    render() {
        const { fullSchedule } = this.state;
        let dispText = "Routes..";
        let contents = <StopRouteInfoTableDisplay
            fullSchedule={fullSchedule}
            text={dispText} />
        return (
            <div>
                <button onClick={this.handleClick}>
                    {'Close Socket'}
                </button>
                {contents}
            </div>
            
        );
    }
}

