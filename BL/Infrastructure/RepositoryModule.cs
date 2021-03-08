using DAL;
using Ninject.Modules;

namespace BL
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Vehicle>>().To<VehicleRepository>();
        }
    }
}
