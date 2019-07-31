using System.Collections.Generic;
using BusScheduleSevices.Interfaces;
using BusScheduleSevices.Models;
using BusScheduleApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BusScheduleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusStopsController : ControllerBase
    {
        private IBusStopService _busStopService { get; }

        public BusStopsController(IBusStopService busStopService)
        {
            _busStopService = busStopService;
        }

        // GET: api/Stops
        [HttpGet]
        public ActionResult<IEnumerable<BusStopRouteDto>> Get()
        {
            List<BusStopRouteDto> dto = new List<BusStopRouteDto>();
            List<BusStop> busStops = _busStopService.GetAllBusStops();

            foreach (BusStop stop in busStops)
            {
                dto.Add(new BusStopRouteDto {
                    BusStop = stop.StopName,
                    BusStopNumber = stop.StopNumber
                });
            }
            return dto;
        }

        // GET: api/Stops/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Stops
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Stops/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
