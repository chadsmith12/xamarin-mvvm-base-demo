﻿using Ninject;
using Ninject.Modules;
using TripLog.Modules;
using TripLog.ViewModels;
using TripLog.Views;
using Xamarin.Forms;

namespace TripLog
{
    public partial class App : Application
    {
        public IKernel Kernel { get; set; }
        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();
            var mainPage = new NavigationPage(new MainPage());

            // Register core services
            Kernel = new StandardKernel(new CoreModules(), new NavigationModule(mainPage.Navigation));

            // Register platform specific services
            Kernel.Load(platformModules);

            // get the MainViewModel from ninject
            mainPage.BindingContext = Kernel.Get<MainViewModel>();


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

