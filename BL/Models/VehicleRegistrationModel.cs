using RaceTrack.Common;
using System.ComponentModel.DataAnnotations;

namespace BL
{
    public class VehicleRegistrationModel
    {
        [Required]
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }
        [Display(Name = "Vehicle Type")]
        public VehicleType VehicleType { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        public int Year { get; set; }
        [Display(Name = "Tow Strap Available")]
        public bool IsTowStrapAvailable { get; set; }
        [Display(Name = "Lifting Height, in")]
        public double LiftingHeight { get; set; }
        [Display(Name = "Tire Wear, %")]
        public int TireWear { get; set; }
    }
}
