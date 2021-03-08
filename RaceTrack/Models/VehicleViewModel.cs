using RaceTrack.Common;
using System.ComponentModel.DataAnnotations;

namespace RaceTrack.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }
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
    }
}