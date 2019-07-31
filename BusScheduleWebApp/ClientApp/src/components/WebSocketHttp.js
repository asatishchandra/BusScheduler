import React, { Component } from 'react';
import PropTypes from 'prop-types';
import StopRouteInfoTableDisplay from './StopRouteInfoTableDisplay';

class WebSocketHttp extends Component {

    constructor(props) {
        super(props);
        console.log(this.props.wsUrl);
        let wsUrl = this.props.wsUrl;
        let apiUrl = this.props.apiUrl;
        let socket = new WebSocket(wsUrl);
        this.state = {
            socket: socket,
            wsUrl: wsUrl,
            apiUrl: apiUrl,
            fullSchedule: [],
            loading: true
        };
    }

    componentDidMount() {
        this.connect();
        this.stopRouteDataByTime();
    }

    async stopRouteDataByTime() {
        const { apiUrl } = this.state;
        await fetch(apiUrl);
        this.setState({ fullSchedule: [], loading: false });
    }

    onMessage = (evt) => {
        this.setState({ fullSchedule: JSON.parse(evt.data) }, () => { console.log('data updated'); });
        console.log(evt.data);
    }

    connect() {
        const { socket, wsUrl } = this.state;

        socket.onopen = function (event) {
            console.log("opened connection to " + wsUrl);
        };

        socket.onclose = function (event) {
            console.log("closed connection from " + wsUrl);
        };

        socket.onmessage = this.onMessage;

        socket.onerror = function (event) {
            console.log("error: " + event.data);
        };
    }

    render() {
        const { fullSchedule } = this.state;
        let dispText = "Routes..";
        
        return (
            <StopRouteInfoTableDisplay
                fullSchedule={fullSchedule}
                text={dispText} />
        );
    }

    componentWillUnmount() {
        
    }
}


WebSocketHttp.propTypes = {
    wsUrl: PropTypes.string.isRequired,
    apiUrl: PropTypes.string.isRequired
};

WebSocketHttp.defaultProps = {
};

export default WebSocketHttp;