using TripLog.Controls;
using TripLog.Interfaces;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Views
{
    public class NewEntryPage : ContentPage
    {
        public NewEntryPage()
        {
            Title = "New Entry";

            // Form Fields
            var title = new EntryCell {Label = "Title"};
            var latitude = new EntryCell {Label = "Latitude", Keyboard = Keyboard.Numeric};
            var longitude = new  EntryCell {Label = "Longitude", Keyboard = Keyboard.Numeric};
            var date = new DatePickerEntryCell {Label = "Date "};
            var rating = new EntryCell {Label = "Rating", Keyboard = Keyboard.Numeric};
            var notes = new EntryCell {Label = "Notes"};
            // toolbar
            var save = new ToolbarItem { Text = "Save" };
            // set bindings
            title.SetBinding(EntryCell.TextProperty, "Title", BindingMode.TwoWay);
            latitude.SetBinding(EntryCell.TextProperty, "Latitude", BindingMode.TwoWay);
            longitude.SetBinding(EntryCell.TextProperty, "Longitude", BindingMode.TwoWay);
            date.SetBinding(DatePickerEntryCell.DateProperty, "Date", BindingMode.TwoWay);
            rating.SetBinding(EntryCell.TextProperty, "Rating", BindingMode.TwoWay);
            notes.SetBinding(EntryCell.TextProperty, "Notes", BindingMode.TwoWay);
            save.SetBinding(MenuItem.CommandProperty, "SaveCommand");

            // Form
            var entryForm = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection()
                    {
                        title, latitude, longitude, date, rating, notes
                    }
                }
            };

            //entryForm.SetBinding<NewEntryViewModel>(TableView.IsVisibleProperty, p => p.IsBusy);
            // loading
            var loading = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new ActivityIndicator {IsRunning = true},
                    new Label {Text = "Getting Current Location..."}
                }
            };

            loading.SetBinding<NewEntryViewModel>(StackLayout.IsVisibleProperty, p => p.IsBusy);
            var mainLayout = new Grid {Children = { entryForm, loading}};

            ToolbarItems.Add(save);

            Content = mainLayout;
        }
    }
}
