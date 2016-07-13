using System;
using System.Collections.Generic;
using TripLog.Interfaces;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Views
{
    public class MainPage : ContentPage
    {
        private MainViewModel Vm => BindingContext as MainViewModel;

        public MainPage()
        {
            Title = "Triplog";
            BindingContext = new MainViewModel(DependencyService.Get<INavigationService>());

            var itemTemplate = new DataTemplate(typeof(TextCell));
            itemTemplate.SetBinding(TextCell.TextProperty, "Title");
            itemTemplate.SetBinding(TextCell.DetailProperty, "Notes");

            // toolbar
            var newButton = new ToolbarItem {Text = "New"};
            newButton.SetBinding(MenuItem.CommandProperty, "NewCommand");
            ToolbarItems.Add(newButton);

            var entries = new ListView
            {
                ItemTemplate = itemTemplate
            };

            entries.SetBinding(ListView.ItemsSourceProperty, "LogEntries");
            entries.ItemTapped += (sender, e) =>
            {
                var item = (TripLogEntry) e.Item;
                Vm.ViewCommand.Execute(item);
            };

            Content = entries;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // init the main view model
            if (Vm != null)
                await Vm.Init();
        }
    }
}


