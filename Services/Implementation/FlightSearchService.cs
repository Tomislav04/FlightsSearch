using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Services.Interfaces;
using Services.ViewModels;
using Services.Mappers;
using System.Security.Cryptography;
using System.Text;
using Services.Helpers;
using RestSharp;
using Newtonsoft.Json;

namespace Services.Implementation
{

    public class FlightSearchService : IFlightSearchService
    {
        private IFlightSearchRepository _flightSearchRepository;
        private readonly IMemoryCache _cache;
        private readonly AmadeusTokenHelper _tokenHelper;

        public FlightSearchService(IFlightSearchRepository flightSearchRepository,
            IMemoryCache cache,
            AmadeusTokenHelper tokenHelper)
        {
            _flightSearchRepository = flightSearchRepository;
            _cache = cache;
            _tokenHelper = tokenHelper;
        }

        public async Task<List<AmadeusFlightSearchResultViewModel>> SearchFlightsAsync(FlightSearchRequest request)
        {
            var flightSearchRequestId = await _flightSearchRepository.SaveNewFlightsSearchRequestAsync(request);

            var results = new List<AmadeusFlightSearchResultViewModel>();
            var cacheKey = GenerateCacheKey(request);
            if (_cache.TryGetValue(cacheKey, out List<AmadeusFlightSearchResultViewModel> cachedResults))
            {
                return cachedResults; 
            }

            var databaseResults =  await _flightSearchRepository.SearchFlightsInDatabase(request);
            results = databaseResults.Select(x => FlightSearchMapper.MapToAmadeusFlightSearchResultViewModel(x)).ToList();
            if (results.Any())
            {
                _cache.Set(cacheKey, results, TimeSpan.FromMinutes(10));
                return results;
            }

            results = await SearchFlightsFromAmadeusAsync(request);

            if (results.Any())
            {
                var newResults = results.Select(x => FlightSearchMapper.MapToFlightSearchResult(x, flightSearchRequestId)).ToList();
                await _flightSearchRepository.SaveNewFlightsSearchResultsAsync(newResults);
                _cache.Set(cacheKey, results, TimeSpan.FromMinutes(10));
            }

            return results;
        }

        private async Task<List<AmadeusFlightSearchResultViewModel>> SearchFlightsFromAmadeusAsync(FlightSearchRequest request)
        {
            string token = await _tokenHelper.GetAccessTokenAsync();

            var client = new RestClient("https://test.api.amadeus.com");
            var restRequest = new RestRequest("/v2/shopping/flight-offers", Method.Get);
            restRequest.AddHeader("Authorization", $"Bearer {token}");
            restRequest.AddParameter("originLocationCode", request.Origin);
            restRequest.AddParameter("destinationLocationCode", request.Destination);
            restRequest.AddParameter("departureDate", request.DepartureDate.ToString("yyyy-MM-dd"));
            restRequest.AddParameter("returnDate", request.ReturnDate?.ToString("yyyy-MM-dd"));
            restRequest.AddParameter("adults", request.AdultsPassengers);
            restRequest.AddParameter("children", request.KidsPassengers);
            restRequest.AddParameter("currencyCode", request.Currency);

            RestResponse response = await client.ExecuteAsync(restRequest);

            if (!response.IsSuccessful)
                throw new Exception("Failed to search flights.");

            var results = new List<AmadeusFlightSearchResultViewModel>();
            var flightOfferResponse = JsonConvert.DeserializeObject<AmadeusFlightOfferResponseViewModel>(response.Content);

            var flightSearchResults = flightOfferResponse.Data.Select(flightOffer =>
            {
                var firstSegment = flightOffer.Itineraries.FirstOrDefault()?.Segments.FirstOrDefault();
                var lastSegment = flightOffer.Itineraries.LastOrDefault()?.Segments.FirstOrDefault();

                return new AmadeusFlightSearchResultViewModel
                {
                    Origin = request.Origin,
                    Destination = request.Destination,
                    DepartureDate = firstSegment?.Departure?.At.ToString("dd.MM.yyyy hh:mm:ss") ?? DateTime.MinValue.ToString("dd.MM.yyyy hh:mm:ss"),
                    ReturnDate = lastSegment?.Departure.At.ToString("dd.MM.yyyy hh:mm:ss") ?? "",
                    StopoversDeparture = firstSegment?.NumberOfStops ?? 0,
                    StopoversReturn = lastSegment?.NumberOfStops ?? 0,
                    PassengersNumber = flightOffer.NumberOfBookableSeats,
                    Currency = flightOffer.Price.Currency,
                    TotalPrice = decimal.TryParse(flightOffer.Price.GrandTotal.Replace(".", ","), out var totalPrice) ? totalPrice : 0m
                };
            }).ToList();

            return flightSearchResults;
        }

        private string GenerateCacheKey(FlightSearchRequest searchRequest)
        {
            var keyString = $"{searchRequest.Origin}-{searchRequest.Destination}-{searchRequest.DepartureDate}-{searchRequest.ReturnDate}-{searchRequest.AdultsPassengers}-{searchRequest.KidsPassengers}-{searchRequest.Currency}";
            return GenerateHash(keyString);
        }

        private string GenerateHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
