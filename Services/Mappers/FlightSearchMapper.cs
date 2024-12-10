using Domain.Models;
using Services.ViewModels;

namespace Services.Mappers
{
    public class FlightSearchMapper
    {
        public static FlightSearchResult MapToFlightSearchResult(AmadeusFlightSearchResultViewModel viewModel, int flightSearchRequestId)
        {
            var parseDepartureDate = DateTime.TryParse(viewModel.DepartureDate, out DateTime _departureDate);
            var parseReturnDate = DateTime.TryParse(viewModel.ReturnDate, out DateTime _returnDate);

            var model = new FlightSearchResult
            {
                Origin = viewModel.Origin,
                Destination = viewModel.Destination,
                DepartureDate = _departureDate,
                ReturnDate = _returnDate,
                StopoversDeparture = viewModel.StopoversDeparture,
                StopoversReturn = viewModel.StopoversReturn,
                PassengersNumber = viewModel.PassengersNumber,
                TotalPrice = viewModel.TotalPrice,
                FlightSearchRequestId = flightSearchRequestId,
                CreatedOn = DateTime.Now
            };

            return model;
        }

        public static AmadeusFlightSearchResultViewModel MapToAmadeusFlightSearchResultViewModel(FlightSearchResult viewModel)
        {
            var model = new AmadeusFlightSearchResultViewModel
            {
                Origin = viewModel.Origin,
                Destination = viewModel.Destination,
                DepartureDate = viewModel.DepartureDate.ToString("dd-MM-yyyy hh:mm:ss"),
                ReturnDate = viewModel.ReturnDate?.ToString("dd-MM-yyyy hh:mm:ss"),
                StopoversDeparture = viewModel.StopoversDeparture,
                StopoversReturn = viewModel.StopoversReturn,
                PassengersNumber = viewModel.PassengersNumber,
                TotalPrice = viewModel.TotalPrice,
            };

            return model;
        }
    }
}
