using FlyNewShore.Business;
using FlyNewShore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlyNewShore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightBL<Journey> _flight;

        public FlightController(IFlightBL<Journey> flightModel)
        {
            this._flight = flightModel;
        }

        [HttpGet]
        [Route("{origin}/{destination}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<Journey>> Get(string origin, string destination)
        {
            return await _flight.GetJourney(origin, destination);
        }

        //public async Task<IEnumerable<Journey>> Get([FromBody]Request request)
        //{
        //    return await _flight.GetJourney(request.Origin, request.Destination);
        //}

    }
}
