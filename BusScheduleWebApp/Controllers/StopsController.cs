using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusScheduleSevices.Interfaces;
using BusScheduleSevices.Models;
using BusScheduleWebApp.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusScheduleWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StopsController : ControllerBase
    {
        private IBusStopService _busStopService { get; }

        public StopsController(IBusStopService busStopService)
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
