import React, { Component } from 'react';
import PropTypes from 'prop-types';
import StopRouteInfoTableDisplay from './StopRouteInfoTableDisplay';

export class WebSocketHttp extends Component {

    constructor(props) {
        super(props);
        console.log(this.props.wsUrl);
        let wsUrl = this.props.wsUrl;
        let apiUrl = this.props.apiUrl;
        let socket = this.props.socket;
        let fullSchedule = this.props.fullSchedule;
        let dispText = this.props.dispText
        this.state = {
            socket: socket,
            wsUrl: wsUrl,
            apiUrl: apiUrl,
            fullSchedule: fullSchedule,
            loading: true,
            isSocketOpen: true,
            dispText: dispText
        };
        //debugger;
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

    async stopRouteDataByTime() {
        const { apiUrl } = this.state;
        await fetch(apiUrl);
        this.setState({ fullSchedule: [], loading: false });
        //debugger;
    }

    handleClick = () => {
        //const { isSocketOpen } = this.state;
        this.onClose();
        //isSocketOpen ? this.onClose() : this.onOpen();
        //this.setState({ isSocketOpen: !isSocketOpen });
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
        this.setState({ fullSchedule: JSON.parse(evt.data) }, () => {
            console.log('onMessage');
        });
        console.log(evt.data);
    }

    onError = (evt) => {
        console.log("error: " + evt.data);
    }

    dispTable() {
        const { fullSchedule, dispText } = this.state;
        return (
            <div>
                <h1>{dispText}</h1>
                <table className='table table-striped'>
                    <thead>
                        <tr>
                            <th>Stop</th>
                            <th>Route Schedule</th>
                        </tr>
                    </thead>
                    <tbody>
                        {fullSchedule.map(stop =>
                            <tr key={stop.busStop}>
                                <td>{stop.busStop}</td>
                                <td>
                                    <table>
                                        <thead>
                                            <tr>
                                                <th>Route</th>
                                                <th>Schedule</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {stop.busRoutes.map(routeInfo =>
                                                <tr key={routeInfo.routeName}>
                                                    <td>{routeInfo.routeName}</td>
                                                    <td>{routeInfo.schedule.join(", ")}</td>
                                                </tr>
                                            )}
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
        
    }

    render() {
        const { fullSchedule, loading, dispText } = this.state;
        let contents = loading
            ? <p><em>Loading...</em></p>
            : this.dispTable()
        return (
            <div>
                <button onClick={this.handleClick}>
                    {'Close Socket'}
                </button>
                { contents }
            </div>
        );
    }

    componentWillUnmount() {
        this.onClose();
        console.log("unmount"); 
    }
}


WebSocketHttp.propTypes = {
    wsUrl: PropTypes.string.isRequired,
    apiUrl: PropTypes.string.isRequired,
    socket: PropTypes.object.isRequired,
    isSocketOpen: PropTypes.bool.isRequired,
    fullSchedule: PropTypes.array.isRequired,
    dispText: PropTypes.string.isRequired
};

WebSocketHttp.defaultProps = {
};

export default WebSocketHttp;