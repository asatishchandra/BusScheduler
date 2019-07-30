import React, { Component } from 'react';
import WebSocketHttp from './WebSocketHttp';

export class FetchData extends Component {
  

  

      render() {
        let port = document.location.port ? (":" + document.location.port) : "";
        //let wsUrl = "ws://" + document.location.hostname + port + "/ws";
          let wsUrl = "ws://localhost:62673/ws";
        let apiUrl = "http://localhost:62673/api/Buses/time";

        //let contents = this.state.loading
        //    ? <p><em>Loading...</em></p>
        //    : <WebSocketHttp wsUrl={wsUrl} apiUrl={apiUrl} />

        return (
            <WebSocketHttp wsUrl={wsUrl} apiUrl={apiUrl} />
        );
    }
    
    
}

