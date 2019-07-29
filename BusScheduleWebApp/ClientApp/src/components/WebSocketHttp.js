﻿import React, { Component } from 'react';
import PropTypes from 'prop-types';
import SockJS from 'sockjs-client';
import Stomp from 'stompjs';

class WebSocketHttp extends Component {

    constructor(props) {
        super(props);
        console.log(this.props.url);
        let socket = new SockJS(this.props.url);
        let stompClient = Stomp.over(socket);
        this.state = {
            socket: socket,
            stompClient: stompClient
        };
    }

    componentDidMount() {
        //let sock = this.state.socket;
        let sock = new SockJS(this.props.url);
        debugger;
        sock.onopen = function () {
            debugger;
            console.log('open');
            sock.send('test');
        };

        sock.onmessage = function (e) {
            debugger;
            console.log('message', e.data);
            sock.close();
        };

        sock.onclose = function () {
            debugger;
            console.log('close');
        };
        //let stompClient = this.state.stompClient;

        //if (this.props.listenerPath !== undefined) {
        //    debugger;
        //    stompClient.connect({}, (frame) => {
        //        console.log('WebSocketHttp Connected: ' + frame);
        //        debugger;
        //        stompClient.subscribe(this.props.listenerPath, (message) => this.props.onMessage(message));
        //    });
        //}

        //stompClient.onclose = () => {
        //    console.log('WebSocketHttp disconnected');
        //    if (typeof this.props.onClose === 'function')
        //        this.props.onClose();
        //}
    }

    //sendMessage(message) {
    //    if (this.props.senderPath !== undefined) {
    //        let message = "Hello from client";
    //        this.state.stompClient.send(this.props.senderPath, {}, message);
    //    }
    //}

    componentWillUnmount() {
        let socket = this.state.socket;
        socket.close();
    }

    render() {
        return (
            <div></div>
        );
    }
}

WebSocketHttp.propTypes = {
    url: PropTypes.string.isRequired,
    //senderPath: PropTypes.string.isRequired,
    //listenerPath: PropTypes.string.isRequired,
    //onMessage: PropTypes.func.isRequired,
    //onClose: PropTypes.func
};

WebSocketHttp.defaultProps = {
};

export default WebSocketHttp;