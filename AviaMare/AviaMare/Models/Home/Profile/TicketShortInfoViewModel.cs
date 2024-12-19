namespace AviaMare.Models.Home.Profile
{
    public class TicketShortInfoViewModel
    {
        public string Destination { get; set; }
        public string Departure { get; set; }
        public int IdPlane { get; set; }
        public int Time { get; set; }
        public DateTime TakeOffTime { get; set; }
        public DateTime LandingTime { get; set; }
    }
}
