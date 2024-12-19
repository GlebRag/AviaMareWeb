using AviaMare.Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaMare.Data.Models
{
    public class TicketShortInfo : BaseModel
    {
        public string Destination { get; set; }
        public string Departure { get; set; }
        public int IdPlane { get; set; }
        public int Time { get; set; }
        public decimal Cost { get; set; }
        public DateTime TakeOffTime { get; set; }
        public DateTime LandingTime { get; set; }
        public int Count { get; set; }
    }
}
