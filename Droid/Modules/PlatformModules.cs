using Ninject.Modules;
using TripLog.Droid.Services;
using TripLog.Interfaces;

namespace TripLog.Droid.Modules
{
    public class PlatformModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ILocationService>().To<LocationService>().InSingletonScope();
        }
    }
}