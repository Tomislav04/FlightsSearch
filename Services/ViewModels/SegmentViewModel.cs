using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class SegmentViewModel
    {
        public DepartureViewModel Departure { get; set; }
        public ArrivalViewModel Arrival { get; set; }
        public int NumberOfStops { get; set; }
    }
}
