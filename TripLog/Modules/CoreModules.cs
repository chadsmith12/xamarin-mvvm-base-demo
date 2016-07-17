using System;
using Ninject.Modules;
using TripLog.Interfaces;
using TripLog.Services;
using TripLog.ViewModels;

namespace TripLog.Modules
{
    public class CoreModules : NinjectModule
    {
        public override void Load()
        {
            // ViewModels
            Bind<MainViewModel>().ToSelf();
            Bind<DetailViewModel>().ToSelf();
            Bind<NewEntryViewModel>().ToSelf();

            // Services
            var tripLogService = new TripLogApiService(new Uri("http://triplogapp-demo.azurewebsites.net"));

            Bind<ITripLogApiService>().ToMethod(x => tripLogService).InSingletonScope();
        }
    }
}
