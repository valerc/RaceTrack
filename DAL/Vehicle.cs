using RaceTrack.Common;

namespace DAL
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public VehicleType VehicleType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}
