using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using BusScheduleWebApp.DTO;
using BusScheduleWebApp.SocketManager;
using BusScheduleSevices.Interfaces;
using BusScheduleSevices.Models;
using BusScheduleSevices.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BusScheduleWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private IBusScheduleService _busScheduleService { get; }
        private readonly ScheduleSocketManager _socketManager;
        private List<BusStopRouteDto> _dto;
        public BusesController(IBusScheduleService busScheduleService, ScheduleSocketManager socketManager)
        {
            _busScheduleService = busScheduleService;
            _socketManager = socketManager;
        }

        // GET api/busues
        [HttpGet]
        public ActionResult<IEnumerable<BusStopRouteDto>> Get()
        {
            List<BusStopRouteDto> dto = new List<BusStopRouteDto>();
            List<BusStop> busStopsAndRoutes = _busScheduleService.GetAllStopRouteData();
            
            foreach (BusStop stop in busStopsAndRoutes)
            {
                List<BusRouteDto> busRoutes = new List<BusRouteDto>();
                foreach (KeyValuePair<BusRoute, List<string>> route in stop.StopSchedule)
                {
                    busRoutes.Add(new BusRouteDto {
                        RouteName = route.Key.RouteName,
                        Schedule = route.Value
                    });
                }
                dto.Add(new BusStopRouteDto {
                    BusStop = stop.StopName,
                    BusStopNumber = stop.StopNumber,
                    BusRoutes = busRoutes
                });
            }
            return dto;
        }

        // GET api/buses/3:01
        [HttpGet("{time}")]
        public async Task<IActionResult> Get(string time)
        {
            try
            {
                Timer timer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds)
                {
                    AutoReset = true
                };
                timer.Elapsed += new ElapsedEventHandler(GetDto);
                timer.Start();
                _dto = GetDto();
                await _socketManager.SendMessageToAllAsync(JsonConvert.SerializeObject(_dto));
                return Ok();
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
        private void GetDto(object sender, ElapsedEventArgs e)
        {
            _dto = GetDto();
        }
        private List<BusStopRouteDto> GetDto() {
            List<BusStopRouteDto> dto = new List<BusStopRouteDto>();
            string timeNow = DateTime.Now.ToString("HH:mm");
            List<BusStop> busStopsAndRoutes = _busScheduleService.GetNextTwoBusArrivalDataByTime(timeNow);

            foreach (BusStop stop in busStopsAndRoutes)
            {
                List<BusRouteDto> busRoutes = new List<BusRouteDto>();
                foreach (KeyValuePair<BusRoute, List<string>> route in stop.StopSchedule)
                {
                    busRoutes.Add(new BusRouteDto {
                        RouteName = route.Key.RouteName,
                        Schedule = route.Value
                    });
                }
                dto.Add(new BusStopRouteDto {
                    BusStop = stop.StopName,
                    BusStopNumber = stop.StopNumber,
                    BusRoutes = busRoutes
                });
            }
            return dto;
        }

        // GET api/buses/1/3:01
        [HttpGet("{stopId}/{time}")]
        public ActionResult<List<BusStopRouteDto>> Get(int stopId, string time)
        {
            string timeNow = DateTime.Now.ToString("HH:mm");
            List<BusStopRouteDto> dto = new List<BusStopRouteDto>();
            List<BusStop> busStopsAndRoutes = _busScheduleService.GetNextTwoBusArrivalDataByTime(timeNow);
            BusStop requestedStop = busStopsAndRoutes.Where(e => e.StopNumber == stopId).FirstOrDefault();

            List<BusRouteDto> busRoutes = new List<BusRouteDto>();
            foreach (KeyValuePair<BusRoute, List<string>> route in requestedStop.StopSchedule)
            {
                busRoutes.Add(new BusRouteDto {
                    RouteName = route.Key.RouteName,
                    Schedule = route.Value
                });
            }
            dto.Add(new BusStopRouteDto {
                BusStop = requestedStop.StopName,
                BusStopNumber = requestedStop.StopNumber,
                BusRoutes = busRoutes
            });
            return dto;
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