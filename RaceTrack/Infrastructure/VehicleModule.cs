using BL;
using Ninject.Modules;

namespace RaceTrack
{
    public class VehicleModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleService>().To<VehicleService>();
        }
    }
}