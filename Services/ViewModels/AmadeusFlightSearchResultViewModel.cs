using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class AmadeusFlightSearchResultViewModel
    {
        public string Origin { get; set; }  // Polazni aerodrom

        public string Destination { get; set; }  // Odredišni aerodrom

        public string DepartureDate { get; set; }  // Datum polaska

        public string? ReturnDate { get; set; }  // Datum povratka

        public int StopoversDeparture { get; set; }  // Broj presjedanja

        public int StopoversReturn { get; set; }  // Broj presjedanja

        public int PassengersNumber { get; set; }  // Broj putnika

        public string Currency { get; set; }  // Valuta

        public decimal TotalPrice { get; set; }  // Ukupna cijena

    }
}
