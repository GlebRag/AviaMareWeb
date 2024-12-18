using Enums.Users;
using AviaMare.Data.Interface.Models;
using System.Numerics;

namespace AviaMare.Data.Models
{
    public class TicketData : BaseModel, ITicket
    {
        public string Destination { get; set; }
        public string Departure { get; set; }
        public int IdPlane { get; set; }
        public int Time { get; set; }
        public decimal Cost { get; set; }
        public DateTime TakeOffTime { get; set; }
        public DateTime LandingTime { get; set; }
        public int Count { get; set; }

        public virtual List<UserData> Buyers { get; set; } = new List<UserData>();
    }
}
