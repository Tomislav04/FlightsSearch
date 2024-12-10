

using Domain.Models;

namespace Domain.Interfaces
{
    public interface IFlightSearchRepository
    {
        Task<List<FlightSearchResult>> SearchFlightsInDatabase(FlightSearchRequest request);

        Task SaveNewFlightsSearchResultsAsync(List<FlightSearchResult> results);

        Task<int> SaveNewFlightsSearchRequestAsync(FlightSearchRequest request);
    }
}
