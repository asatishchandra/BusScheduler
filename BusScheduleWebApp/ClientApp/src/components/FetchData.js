import React, { Component } from 'react';
import WebSocketHttp from './WebSocketHttp';
var message = "";
export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.dateFormatted}>
              <td>{forecast.dateFormatted}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

      return (
          <div>
              <WebSocketHttp url="http://localhost:62673/api/buses/3:01"
                  senderPath="/3:01"
                  listenerPath="/3:01"
                  onMessage={(message) => this.onMessage(message)}
                  ref={WebSocketHttp => { this.refWebSocketHttp = WebSocketHttp }} />
            <h1>Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
          </div>
    );
  }
    onMessage(message) {
        debugger;
    }

    sendMessage() {
        let user = "web client"
        this.refWebSocketHttp.sendMessage(message);
    }

    async populateWeatherData() {
        //const response1 = await fetch('http://localhost:62673/api/Buses/1/3:01');
        const response = await fetch('api/SampleData/WeatherForecasts');
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }
}
//const response = await fetch('https://localhost:44305/api/Buses/1/3:01');

