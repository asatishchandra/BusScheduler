using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusScheduleApi.DTO;
using BusScheduleSevices.Models;
using BusScheduleSevices.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusScheduleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        public BusScheduleService busScheduleService;

        public BusesController()
        {
            busScheduleService = new BusScheduleService();
        }
        // GET api/busues
        [HttpGet]
        public ActionResult<IEnumerable<BusStopRouteDto>> Get()
        {
            List<BusStopRouteDto> dto = new List<BusStopRouteDto>();
            List<BusStop> busStopsAndRoutes = busScheduleService.GetAllStopRouteData();

            foreach (var stop in busStopsAndRoutes)
            {
                dto.Add(new BusStopRouteDto { BusStop = stop.StopName, BusRoutes = stop.StopSchedule });
            }
            return dto;
        }

        // GET api/buses/3:01
        [HttpGet("{time}")]
        public ActionResult<IEnumerable<BusStopRouteDto>> Get(string time)
        {
            List<BusStopRouteDto> dto = new List<BusStopRouteDto>();
            List<BusStop> busStopsAndRoutes = busScheduleService.GetNextTwoBusArrivalDataByTime(time);

            foreach (var stop in busStopsAndRoutes)
            {
                dto.Add(new BusStopRouteDto { BusStop = stop.StopName, BusRoutes = stop.StopSchedule });
            }
            return dto;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}