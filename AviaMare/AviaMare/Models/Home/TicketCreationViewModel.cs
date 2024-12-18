using System.ComponentModel.DataAnnotations;

namespace AviaMare.Models.Home
{
    public class TicketCreationViewModel
    {
        [Required]
        public string Destination { get; set; }
        [Required]
        public string Departure { get; set; }
        [Required]
        public int IdPlane { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public DateTime TakeOffTime { get; set; }
        [Required]
        public DateTime LandingTime { get; set; }
        public int Count { get; set; }
    }
}
