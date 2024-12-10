using Domain.Models;
using Services.ViewModels;

namespace Services.Interfaces
{
    public interface IFlightSearchService
    {
        Task<List<AmadeusFlightSearchResultViewModel>> SearchFlightsAsync(FlightSearchRequest request);
    }
}
