import React, { Component } from 'react';
import StopRouteInfoTableDisplay from './StopRouteInfoTableDisplay';
//import { WebSocketHttp } from './WebSocketHttp';
//import { WebSocketHTTPClass } from '../custom/WebSocketHTTPClass';

export class RoutesByCurrentTime extends Component {
    constructor(props) {
        super(props);
        //const wsApiUrl = "http://localhost:62673/api/BusTimeSocket";
        const initApiUrl = "http://localhost:62673/api/Buses/time";
        //const wsUrl = "ws://localhost:62673/wsTime";
        //const webSocket = new WebSocketHTTPClass(wsUrl);
        this.state = {
            //socket: webSocket.Socket,
            //isSocketOpen: true,
            //wsUrl: wsUrl,
            //wsApiUrl: wsApiUrl,
            initApiUrl: initApiUrl,
            fullSchedule: [],
            loading: true
        };
    } 

    componentDidMount() {
        this.stopRouteDataByTime();
    }

    async stopRouteDataByTime() {
        const { initApiUrl } = this.state;
        let response = await fetch(initApiUrl);
        let data = await response.json();
        this.setState({ fullSchedule: data, loading: false }, () => {
            console.log(this.state.fullSchedule);
        });
    }

    componentWillUnmount() {
        //this.refWebSocketHttp.onClose();
        console.log("unmount");
    }
    //render() {
    //    const { wsUrl, wsApiUrl, socket, isSocketOpen, fullSchedule, loading } = this.state;
    //    let dispText = "Routes By Time"
    //    let busData = loading
    //        ? <p><em>Loading...</em></p>
    //        : <WebSocketHttp
    //            fullSchedule={fullSchedule}
    //            socket={socket}
    //            isSocketOpen={isSocketOpen}
    //            apiUrl={wsApiUrl}
    //            wsUrl={wsUrl}
    //            dispText={dispText}
    //            ref={WebSocketHttp => { this.refWebSocketHttp = WebSocketHttp }} />
    //    return (
    //        <div>
    //            {busData}
    //        </div>
    //    )
    //}
    render() {
        const { fullSchedule, loading } = this.state;
        let dispText = "Routes For Current Time"
        let contents = loading
            ? <p><em>Loading...</em></p>
            : <StopRouteInfoTableDisplay
                fullSchedule={fullSchedule}
                text={dispText} />
        return (
            <div>
                {contents}
            </div>
        )
    }

     
}

