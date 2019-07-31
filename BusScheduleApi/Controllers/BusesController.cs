using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using BusScheduleApi.DTO;
using BusScheduleApi.SocketManager;
using BusScheduleSevices.Interfaces;
using BusScheduleSevices.Models;
using BusScheduleSevices.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BusScheduleApi.Handlers;

namespace BusScheduleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private IBusScheduleService _busScheduleService { get; }
        private BusesMessageHandler _busesMessageHandler;
        private List<BusStopRouteDto> _dto;
        public BusesController(IBusScheduleService busScheduleService, BusesMessageHandler handler)
        {
            _busScheduleService = busScheduleService;
            _busesMessageHandler = handler;
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
        public IActionResult Get(string time)
        {
            try
            {
                Timer timer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds)
                {
                    AutoReset = true
                };
                timer.Elapsed += new ElapsedEventHandler(GetDtoAsync);
                timer.Start();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        // GET api/buses/1/3:01
        [HttpGet("{stopId}/{time}")]
        [EnableCors("EnableCORS")]
        public ActionResult<List<BusStopRouteDto>> Get(int stopId, string time)
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

        #region private methods
        private async void GetDtoAsync(object sender, ElapsedEventArgs e)
        {
            _dto = GetDto();
            await _busesMessageHandler.SendMessageToAllAsync(JsonConvert.SerializeObject(_dto));
        }
        private List<BusStopRouteDto> GetDto()
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
        #endregion
    }
}