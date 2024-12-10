using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class FlightOfferViewModel
    {
        public List<ItineraryViewModel> Itineraries { get; set; }
        public PriceViewModel Price { get; set; }
        public int NumberOfBookableSeats { get; set; }
    }
}
