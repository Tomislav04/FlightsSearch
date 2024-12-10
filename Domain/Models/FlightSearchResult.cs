

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class FlightSearchResult
    {
        [Key]
        public int FlightSearchResultId { get; set; }  // Primarni ključ

        public string Origin { get; set; }  // Polazni aerodrom

        public string Destination { get; set; }  // Odredišni aerodrom

        public DateTime DepartureDate { get; set; }  // Datum polaska

        public DateTime? ReturnDate { get; set; }  // Datum povratka

        public int StopoversDeparture { get; set; }  // Broj presjedanja polazak

        public int StopoversReturn { get; set; }  // Broj presjedanja povratak

        public int PassengersNumber { get; set; }  // Broj putnika

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }  // Ukupna cijena

        public DateTime CreatedOn { get; set; }

        // Strana veze prema FlightSearchRequest
        [ForeignKey("FlightSearchRequest")]
        public int FlightSearchRequestId { get; set; }
    }
}
