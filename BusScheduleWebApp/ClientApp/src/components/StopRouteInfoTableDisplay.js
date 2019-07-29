import React, { Component } from 'react';


export class StopRouteInfoTableDisplay extends Component {

    constructor(props) {
        super(props);
        this.state = {
            fullSchedule: this.props.fullSchedule,
            text: this.props.text
        };
    }

    render() {
        return (
            <div>
                <h1>{ this.props.text }</h1>
                <table className='table table-striped'>
                    <thead>
                        <tr>
                            <th>Stop</th>
                            <th>Route Schedule</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.fullSchedule.map(stop =>
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
}
export default StopRouteInfoTableDisplay;



