import React, { Component } from 'react';
import PropTypes from 'prop-types';


export class StopRouteInfoTableDisplay extends Component {

    render() {
        const { text, fullSchedule } = this.props;

        console.log('stop route info table', fullSchedule);

        return (
            <div>
                <h1>{ text }</h1>
                <table className='table table-striped'>
                    <thead>
                        <tr>
                            <th>Stop</th>
                            <th>Route Schedule</th>
                        </tr>
                    </thead>
                    <tbody>
                        {!fullSchedule ? 'foo bar' : fullSchedule.map(stop =>
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

StopRouteInfoTableDisplay.propTypes = {
    fullSchedule: PropTypes.array.isRequired,
    text: PropTypes.string.isRequired
};

export default StopRouteInfoTableDisplay;



