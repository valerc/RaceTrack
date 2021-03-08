using RaceTrack.Common;
using System.Data.Entity;

namespace DAL
{
    public class VehicleContext : DbContext
    {
        public VehicleContext()
            : base("name=VehicleContext")
        {
            Database.SetInitializer(new VehicleDbInitializer());
        }

        public virtual DbSet<Vehicle> Vehicles { get; set; }
    }

    internal class VehicleDbInitializer : DropCreateDatabaseIfModelChanges<VehicleContext>
    {
        protected override void Seed(VehicleContext db)
        {
            db.Vehicles.Add(new Vehicle
            {
                TeamName = "Crazy beaver",
                Make = "Volkswagen",
                Model = "Beetle",
                Year = 1975,
                VehicleType = VehicleType.Car
            });

            db.Vehicles.Add(new Vehicle
            {
                TeamName = "Quick turtle",
                Make = "Dodge",
                Model = "Ram",
                Year = 1981,
                VehicleType = VehicleType.Truck
            });
        }
    }
}