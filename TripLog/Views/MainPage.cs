using System;
using System.Collections.Generic;
using TripLog.Converters;
using TripLog.Interfaces;
using TripLog.Models;
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

            var itemTemplate = new DataTemplate(typeof(TextCell));
            itemTemplate.SetBinding(TextCell.TextProperty, "Title");
            itemTemplate.SetBinding(TextCell.DetailProperty, "Notes");

            // toolbar
            var newButton = new ToolbarItem {Text = "New"};
            //newButton.SetBinding(MenuItem.CommandProperty, "NewCommand");
            newButton.SetBinding<MainViewModel>(MenuItem.CommandProperty, m => m.NewCommand);
            ToolbarItems.Add(newButton);

            var entries = new ListView
            {
                ItemTemplate = itemTemplate
            };

            entries.SetBinding(ListView.ItemsSourceProperty, "LogEntries", converter: new ReverseBooleanConverter());
            entries.ItemTapped += (sender, e) =>
            {
                var item = (TripLogEntry) e.Item;
                Vm.ViewCommand.Execute(item);
            };

            var loading = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new ActivityIndicator {IsRunning = true},
                    new Label {Text = "Loading Entries..."}
                }
            };

            // set the loading binding to show when busy
            loading.SetBinding<MainViewModel>(StackLayout.IsVisibleProperty, p => p.IsBusy);
            var mainLayout = new Grid {Children = { entries, loading}};

            Content = mainLayout;
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


