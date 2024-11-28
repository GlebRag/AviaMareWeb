using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaMareWinForms
{
    internal class Ticket
    {
        private int id;
        private string destination;
        private string departure;
        private int distance;
        private Plane usedPlane;
        private int time;
        private int cost;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public string Departure
        {
            get { return departure; }
            set { departure = value; }
        }

        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public Plane UsedPlane
        {
            get { return usedPlane; }
            set { usedPlane = value; }
        }

        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public Ticket(int newId, string newDestination, string newDeparture, int newDistance, Plane newPlane, int newTime, int newCost)
        {
            this.Id = newId;
            this.Destination = newDestination;
            this.Departure = newDeparture;
            this.Distance = newDistance;
            this.UsedPlane = newPlane;
            this.Time = newTime;
            this.Cost = newCost;
        }
    }
}
