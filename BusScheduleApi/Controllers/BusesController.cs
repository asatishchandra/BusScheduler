﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusScheduleApi.DTO;
using BusScheduleSevices.Interfaces;
using BusScheduleSevices.Models;
using BusScheduleSevices.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BusScheduleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        public IBusScheduleService BusScheduleService { get; }
        public BusesController(IBusScheduleService busScheduleService)
        {
            BusScheduleService = busScheduleService;
        }

        // GET api/busues
        [HttpGet]
        [EnableCors("EnableCORS")]
        public ActionResult<IEnumerable<BusStopRouteDto>> Get()
        {
            List<BusStopRouteDto> dto = new List<BusStopRouteDto>();
            List<BusStop> busStopsAndRoutes = BusScheduleService.GetAllStopRouteData();
            
            foreach (BusStop stop in busStopsAndRoutes)
            {
                List<BusRouteDto> busRoutes = new List<BusRouteDto>();
                foreach (KeyValuePair<BusRoute, List<string>> route in stop.StopSchedule)
                {
                    busRoutes.Add(new BusRouteDto { RouteName = route.Key.RouteName, Schedule = route.Value });
                }
                dto.Add(new BusStopRouteDto { BusStop = stop.StopName, BusRoutes = busRoutes });
            }
            return dto;
        }

        // GET api/buses/3:01
        [HttpGet("{time}")]
        [EnableCors("EnableCORS")]
        public ActionResult<IEnumerable<BusStopRouteDto>> Get(string time)
        {
            List<BusStopRouteDto> dto = new List<BusStopRouteDto>();
            List<BusStop> busStopsAndRoutes = BusScheduleService.GetNextTwoBusArrivalDataByTime(time);

            foreach (BusStop stop in busStopsAndRoutes)
            {
                List<BusRouteDto> busRoutes = new List<BusRouteDto>();
                foreach (KeyValuePair<BusRoute, List<string>> route in stop.StopSchedule)
                {
                    busRoutes.Add(new BusRouteDto { RouteName = route.Key.RouteName, Schedule = route.Value });
                }
                dto.Add(new BusStopRouteDto { BusStop = stop.StopName, BusRoutes = busRoutes });
            }
            return dto;
        }

        // GET api/buses/1/3:01
        [HttpGet("{stopId}/{time}")]
        [EnableCors("EnableCORS")]
        public ActionResult<BusStopRouteDto> Get(int stopId, string time)
        {
            List<BusStopRouteDto> dto = new List<BusStopRouteDto>();
            List<BusStop> busStopsAndRoutes = BusScheduleService.GetNextTwoBusArrivalDataByTime(time);
            BusStop requestedStop = busStopsAndRoutes.Where(e => e.StopNumber == stopId).FirstOrDefault();

            List<BusRouteDto> busRoutes = new List<BusRouteDto>();
            foreach (KeyValuePair<BusRoute, List<string>> route in requestedStop.StopSchedule)
            {
                busRoutes.Add(new BusRouteDto { RouteName = route.Key.RouteName, Schedule = route.Value });
            }
            return new BusStopRouteDto { BusStop = requestedStop.StopName, BusRoutes = busRoutes };
        }

        // POST api/buses
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/buses/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/buses/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}