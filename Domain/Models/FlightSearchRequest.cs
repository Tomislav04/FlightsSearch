using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class FlightSearchRequest
    {
        [Key]
        public int FlightSearchRequestId { get; set; } // Primarni ključ

        [Required]
        public string Origin { get; set; } // IATA šifra polaznog aerodroma

        [Required]
        public string Destination { get; set; } // IATA šifra odredišnog aerodroma

        [Required]
        public DateTime DepartureDate { get; set; } // Datum polaska

        public DateTime? ReturnDate { get; set; } // Datum povratka

        [Required]
        public int AdultsPassengers { get; set; }  // Broj putnika

        [Required]
        public int KidsPassengers { get; set; }  // Broj putnika

        [Required]
        public string Currency { get; set; } // Valuta pretrage

        public DateTime CreatedOn { get; set; }

        // Navigaciona svojstva
        public ICollection<FlightSearchResult> Results { get; set; }
    }
}
