using TripLog.Interfaces;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;
using Xamarin.Forms;

namespace TripLog
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var mainPage = new NavigationPage(new MainPage());

            // register view models to view mappings for navigation
            var navigationService = DependencyService.Get<INavigationService>() as NavigationService;
            navigationService.Navigation = mainPage.Navigation;

            navigationService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
            navigationService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));
            navigationService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

