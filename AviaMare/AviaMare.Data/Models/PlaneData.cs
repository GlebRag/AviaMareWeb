using Enums.Users;
using AviaMare.Data.Interface.Models;

namespace AviaMare.Data.Models
{
    public class PlaneData : BaseModel, IPlane
    {
        public string Model { get; set; }
        public int Year { get; set; }
        public int BaseSpeed { get; set; }
        public int Seats { get; set; }
    }
}
