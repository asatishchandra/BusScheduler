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
    public class BusStopSocketController : ControllerBase
    {
        
        private IBusScheduleService _busScheduleService { get; }
        private BusesStopHandler _busesStopHandler { get; }

        private static readonly int _refershMinutes = 1;
        private static Timer _timer = new Timer(TimeSpan.FromMinutes(_refershMinutes).TotalMilliseconds);

        public BusStopSocketController(IBusScheduleService busScheduleService, BusesStopHandler stophandler)
        {
            _busScheduleService = busScheduleService;
            _busesStopHandler = stophandler;
        }

        // GET api/BusStopSocket/1/3:01
        [HttpGet("{stopId}")]
        [EnableCors("EnableCORS")]
        public IActionResult Get(int stopId)
        {
            try
            {
                _timer.Elapsed += (sender, e) => GetStopDtoAsync(sender, e, stopId);
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
        
        private async void GetStopDtoAsync(object sender, ElapsedEventArgs e, int stopId)
        {
            List<BusStopRouteDto> _stopDto = GetStopDto(stopId);
            await _busesStopHandler.SendMessageToAllAsync(JsonConvert.SerializeObject(_stopDto));
        }
        private List<BusStopRouteDto> GetStopDto(int stopId)
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
        #endregion
    }
}
