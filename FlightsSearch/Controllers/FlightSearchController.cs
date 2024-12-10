using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.ViewModels;
using System.Globalization;

namespace FlightsSearch.Controllers
{
    [ApiController]
    [Route("FlightsSearch/[controller]")]
    public class FlightSearchController : ControllerBase
    {

        private readonly ILogger<FlightSearchController> _logger;
        private readonly IFlightSearchService _flightSearchService;

        public FlightSearchController(ILogger<FlightSearchController> logger,
            IFlightSearchService flightSearchService)
        {
            _logger = logger;
            _flightSearchService = flightSearchService;
        }

        [HttpGet("SearchFlights")]
        public List<AmadeusFlightSearchResultViewModel> SearchFlights([FromQuery] string origin,
                                                                      [FromQuery] string destination,
                                                                      [FromQuery] DateTime departureDate,
                                                                      [FromQuery] DateTime? returnDate,
                                                                      [FromQuery] int adultsPassengers,
                                                                      [FromQuery] int kidsPassengers,
                                                                      [FromQuery] string currency)
        {
            try
            {
                var request = new FlightSearchRequest
                {
                    Origin = origin,
                    Destination = destination,
                    DepartureDate = departureDate,
                    ReturnDate = returnDate,
                    AdultsPassengers = adultsPassengers,
                    KidsPassengers = kidsPassengers,
                    Currency = currency,
                };
                var data = _flightSearchService.SearchFlightsAsync(request);
                return data.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
