using Azure.Core;
using Domain.Data;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class FlightSearchRepository : IFlightSearchRepository
    {
        private readonly FlightSearchContext _context;
        private readonly ILogger<FlightSearchRepository> _logger;

        public FlightSearchRepository(FlightSearchContext context,
            ILogger<FlightSearchRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task<List<FlightSearchResult>> SearchFlightsInDatabase(FlightSearchRequest request)
        {
            try
            {
                var flightSearchRequestId =  CheckIfRequestExists(request);

                var results = await _context.FlightSearchResults.Where(result => result.FlightSearchRequestId == flightSearchRequestId)
                                                                .ToListAsync();
                return results;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return new List<FlightSearchResult>();
            }            
        }

        public async Task SaveNewFlightsSearchResultsAsync(List<FlightSearchResult> results)
        {
            if (results == null || results.Count == 0)
            {
                return;
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Set<FlightSearchResult>().AddRangeAsync(results.AsEnumerable());
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await transaction.RollbackAsync();
            }
        }

        public async Task<int> SaveNewFlightsSearchRequestAsync(FlightSearchRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var flightSearchRequestId = CheckIfRequestExists(request);
            if (flightSearchRequestId == 0)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    request.CreatedOn = DateTime.Now;
                    await _context.Set<FlightSearchRequest>().AddAsync(request);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return request.FlightSearchRequestId;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    await transaction.RollbackAsync();
                    return 0;
                }
            }
            else
            {
                return flightSearchRequestId;
            }
        }

        private int CheckIfRequestExists(FlightSearchRequest request)
        {
            return  _context.FlightSearchRequests.Where(r =>  r.Origin == request.Origin &&
                                                              r.Destination == request.Destination &&
                                                              r.DepartureDate == request.DepartureDate &&
                                                              r.ReturnDate == request.ReturnDate &&
                                                              r.AdultsPassengers == request.AdultsPassengers &&
                                                              r.KidsPassengers == request.KidsPassengers &&
                                                              r.Currency == request.Currency &&
                                                              r.CreatedOn.Date == request.CreatedOn.Date)?
                                                  .OrderBy(r => r.FlightSearchRequestId)
                                                  .LastOrDefault()?.FlightSearchRequestId ?? 0;
        }
    }
}
