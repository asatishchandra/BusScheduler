using System;
using System.Collections.Generic;
using BusScheduleApi.DTO;
using BusScheduleSevices.Interfaces;
using BusScheduleSevices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BusScheduleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private IBusScheduleService _busScheduleService { get; }
        
        public BusesController(IBusScheduleService busScheduleService)
        {
            _busScheduleService = busScheduleService;
        }

        // GET api/busues
        [HttpGet]
        [EnableCors("EnableCORS")]
        public ActionResult<IEnumerable<BusStopRouteDto>> Get()
        {
            try {
                List<BusStopRouteDto> dto = new List<BusStopRouteDto>();
                List<BusStop> busStopsAndRoutes = _busScheduleService.GetAllStopRouteData();

                foreach (BusStop stop in busStopsAndRoutes)
                {
                    List<BusRouteDto> busRoutes = new List<BusRouteDto>();
                    foreach (KeyValuePair<BusRoute, List<string>> route in stop.StopSchedule)
                    {
                        busRoutes.Add(new BusRouteDto
                        {
                            RouteName = route.Key.RouteName,
                            Schedule = route.Value
                        });
                    }
                    dto.Add(new BusStopRouteDto
                    {
                        BusStop = stop.StopName,
                        BusStopNumber = stop.StopNumber,
                        BusRoutes = busRoutes
                    });
                }
                return dto;
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/buses/3:01
        [HttpGet("{time}")]
        [EnableCors("EnableCORS")]
        public ActionResult<IEnumerable<BusStopRouteDto>> Get(string time)
        {
            try
            {
                List<BusStopRouteDto> dto = new List<BusStopRouteDto>();
                string timeNow = DateTime.Now.ToString("HH:mm");
                List<BusStop> busStopsAndRoutes = _busScheduleService.GetNextTwoBusArrivalDataByTime(timeNow);

                foreach (BusStop stop in busStopsAndRoutes)
                {
                    List<BusRouteDto> busRoutes = new List<BusRouteDto>();
                    foreach (KeyValuePair<BusRoute, List<string>> route in stop.StopSchedule)
                    {
                        busRoutes.Add(new BusRouteDto
                        {
                            RouteName = route.Key.RouteName,
                            Schedule = route.Value
                        });
                    }
                    dto.Add(new BusStopRouteDto
                    {
                        BusStop = stop.StopName,
                        BusStopNumber = stop.StopNumber,
                        BusRoutes = busRoutes
                    });
                }
                return dto;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        // GET api/buses/1/3:01
        [HttpGet("{stopId}/{time}")]
        [EnableCors("EnableCORS")]
        public ActionResult<IEnumerable<BusStopRouteDto>> Get(int stopId, string time)
        {
            try
            {
                string timeNow = DateTime.Now.ToString("HH:mm");
                List<BusStopRouteDto> dto = new List<BusStopRouteDto>();
                BusStop requestedStop = _busScheduleService.GetNextTwoBusArrivalDataByStop(stopId, timeNow);

                List<BusRouteDto> busRoutes = new List<BusRouteDto>();
                foreach (KeyValuePair<BusRoute, List<string>> route in requestedStop.StopSchedule)
                {
                    busRoutes.Add(new BusRouteDto
                    {
                        RouteName = route.Key.RouteName,
                        Schedule = route.Value
                    });
                }
                dto.Add(new BusStopRouteDto
                {
                    BusStop = requestedStop.StopName,
                    BusStopNumber = requestedStop.StopNumber,
                    BusRoutes = busRoutes
                });
                return dto;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

