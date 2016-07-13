using TripLog.Interfaces;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Views
{
    public class NewEntryPage : ContentPage
    {
        public NewEntryPage()
        {
            BindingContext = new NewEntryViewModel(DependencyService.Get<INavigationService>());
            Title = "New Entry";

            // Form Fields
            var title = new EntryCell {Label = "Title"};
            var latitude = new EntryCell {Label = "Latitude", Keyboard = Keyboard.Numeric};
            var longitude = new  EntryCell {Label = "Longitude", Keyboard = Keyboard.Numeric};
            var date = new EntryCell {Label = "Date"};
            var rating = new EntryCell {Label = "Rating", Keyboard = Keyboard.Numeric};
            var notes = new EntryCell {Label = "Notes"};
            // toolbar
            var save = new ToolbarItem { Text = "Save" };
            // set bindings
            title.SetBinding(EntryCell.TextProperty, "Title", BindingMode.TwoWay);
            latitude.SetBinding(EntryCell.TextProperty, "Latitude", BindingMode.TwoWay);
            longitude.SetBinding(EntryCell.TextProperty, "Longitude", BindingMode.TwoWay);
            date.SetBinding(EntryCell.TextProperty, "Date", BindingMode.TwoWay, stringFormat: "{0:d}");
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
            ToolbarItems.Add(save);

            Content = entryForm;
        }
    }
}
