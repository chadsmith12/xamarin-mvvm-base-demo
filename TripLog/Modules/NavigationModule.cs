using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using TripLog.Interfaces;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;
using Xamarin.Forms;

namespace TripLog.Modules
{
    public class NavigationModule : NinjectModule
    {
        private readonly INavigation _navigation;

        public NavigationModule(INavigation navigation)
        {
            _navigation = navigation;
        }
        public override void Load()
        {
            var navigationService = new NavigationService {Navigation = _navigation};

            // Register View Mappings
            navigationService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
            navigationService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));
            navigationService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));

            Bind<INavigationService>().ToMethod(x => navigationService).InSingletonScope();
        }
    }
}
