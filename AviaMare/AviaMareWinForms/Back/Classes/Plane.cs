using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaMareWinForms
{
    internal class Plane
    {
        private int id;
        private string model;
        private int year;
        private int baseSpeed;
        private int seats;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public int BaseSpeed
        {
            get { return baseSpeed; }
            set { baseSpeed = value; }
        }

        public int Seats
        {
            get { return seats; }
            set { seats = value; }
        }

        public Plane(int newId, string newModel, int newYear, int newBaseSpeed, int newSeats)
        {
            this.Id = newId;
            this.Model = newModel;
            this.Year = newYear;
            this.BaseSpeed = newBaseSpeed;
            this.Seats = newSeats;
        }
    }
}
