using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TripLog.Views
{
    public class DetailPage : ContentPage
    {
        public DetailPage(TripLogEntry entry)
        {
            Title = "Entry Details";

            var mainlayout = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition {Height = new GridLength(4, GridUnitType.Star)}, 
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
                }
            };

            // maps
            var map = new Map();
            // center the map around the log entry's location
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(entry.Latitude, entry.Longitude), Distance.FromMiles(.5)));
            // place a pin on the map for the log entry's locations
            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = entry.Title,
                Position = new Position(entry.Latitude, entry.Longitude)
            });

            var title = new Label { HorizontalOptions = LayoutOptions.Center, Text = entry.Title };
            var date = new Label {HorizontalOptions = LayoutOptions.Center, Text = entry.Date.ToString("M")};
            var rating = new Label {HorizontalOptions = LayoutOptions.Center, Text = $"{entry.Rating} star rating"};
            var notes = new Label {HorizontalOptions = LayoutOptions.Center, Text = entry.Notes};
            var details = new StackLayout
            {
                Padding =  10,
                Children = { title, date, rating, notes}
            };

            var detailsBg = new BoxView {BackgroundColor = Color.White, Opacity = .8};

            mainlayout.Children.Add(map);
            mainlayout.Children.Add(detailsBg, 0 ,1);
            mainlayout.Children.Add(details, 0, 1);
            Grid.SetRowSpan(map, 3);

            Content = mainlayout;
        }
    }
}
