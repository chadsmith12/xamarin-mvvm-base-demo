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
            var date = new EntryCell {Label = "Date"};
            var rating = new EntryCell {Label = "Rating", Keyboard = Keyboard.Numeric};
            var notes = new EntryCell {Label = "Notes"};

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

            // toolbar
            var save = new ToolbarItem {Text = "Save"};
            ToolbarItems.Add(save);

            Content = entryForm;
        }
    }
}
