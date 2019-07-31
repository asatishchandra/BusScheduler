using System;
using System.Collections.Generic;
using System.Timers;
using BusScheduleApi.DTO;
using BusScheduleApi.Handlers;
using BusScheduleApi.Misc;
using BusScheduleSevices.Interfaces;
using BusScheduleSevices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BusScheduleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusTimeSocketController : ControllerBase
    {
        private IBusScheduleService _busScheduleService { get; }
        private BusesTimeHandler _busesTimeHandler { get; }

        public BusTimeSocketController(IBusScheduleService busScheduleService, BusesTimeHandler timeHandler)
        {
            _busScheduleService = busScheduleService;
            _busesTimeHandler = timeHandler;
        }

        // GET api/BusTimeSocket
        [HttpGet]
        [EnableCors("EnableCORS")]
        public IActionResult Get()
        {
            try
            {
                Timer _timer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
                Helper.StopExistingTimers(_timer);
                _timer.Elapsed += new ElapsedEventHandler(GetDtoAsync);
                _timer.AutoReset = true;
                _timer.Enabled = true;
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #region private methods
        private async void GetDtoAsync(object sender, ElapsedEventArgs e)
        {
            List<BusStopRouteDto> _dto = GetDto();
            await _busesTimeHandler.SendMessageToAllAsync(JsonConvert.SerializeObject(_dto));
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
