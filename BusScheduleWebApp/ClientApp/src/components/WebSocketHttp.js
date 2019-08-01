import React, { Component } from 'react';
import PropTypes from 'prop-types';
import StopRouteInfoTableDisplay from './StopRouteInfoTableDisplay';
import { WebSocketHTTPClass } from '../custom/WebSocketHTTPClass';

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
    }

    componentDidMount() {
        this.connect();
        this.stopRouteData();
    }

    connect() {
        const { socket } = this.state;
        socket.onopen = this.onOpen;
        socket.onclose = this.onClose;
        socket.onmessage = this.onMessage;
        socket.onerror = this.onError
    }

    async stopRouteData() {
        console.log("stopRouteData");
        const { apiUrl } = this.state;
        await fetch(apiUrl);
        this.setState({ loading: false });
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
            const webSocket = new WebSocketHTTPClass(wsUrl);
            this.setState({ socket: webSocket.Socket }, () => {
                this.connect();
                this.stopRouteData();
                console.log("opened connection to " + wsUrl);
            });
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

    render() {
        const { fullSchedule, loading, dispText, isSocketOpen } = this.state;
        let contents = loading
            ? <p><em>Loading...</em></p>
            : <StopRouteInfoTableDisplay
                fullSchedule={ fullSchedule }
                text={dispText} />
        let buttonText = isSocketOpen ? 'Close Socket' : 'Open Socket';
        return (
            <div>
                <button onClick={this.handleClick}>
                    {buttonText}
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