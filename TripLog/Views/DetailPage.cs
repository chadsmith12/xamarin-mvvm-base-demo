using TripLog.Interfaces;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TripLog.Views
{
    public class DetailPage : ContentPage
    {
        private DetailViewModel Vm => BindingContext as DetailViewModel;
        private readonly Map _map;

        public DetailPage()
        {
            // when he view model has data we update the map
            BindingContextChanged += (sender, args) =>
            {
                if (Vm == null) return;

                Vm.PropertyChanged += (s, e) =>
                {
                    if(e.PropertyName == "Entry")
                        UpdateMap();
                };
            };
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
            _map = new Map();

            var title = new Label { HorizontalOptions = LayoutOptions.Center };
            title.SetBinding(Label.TextProperty, "Entry.Title");
            var date = new Label { HorizontalOptions = LayoutOptions.Center };
            date.SetBinding(Label.TextProperty, "Entry.Date", stringFormat: "{0:M}");
            var rating = new Label { HorizontalOptions = LayoutOptions.Center };
            rating.SetBinding(Label.TextProperty, "Entry.Rating", stringFormat: "{0} star rating");
            var notes = new Label { HorizontalOptions = LayoutOptions.Center };
            notes.SetBinding(Label.TextProperty, "Entry.Notes");
            var details = new StackLayout
            {
                Padding = 10,
                Children = { title, date, rating, notes }
            };

            var detailsBg = new BoxView { BackgroundColor = Color.White, Opacity = .8 };

            mainlayout.Children.Add(_map);
            mainlayout.Children.Add(detailsBg, 0, 1);
            mainlayout.Children.Add(details, 0, 1);
            Grid.SetRowSpan(_map, 3);

            Content = mainlayout;
        }

        private void UpdateMap()
        {
            if (Vm.Entry == null)
                return;


            // center the map around the log entry's location
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Vm.Entry.Latitude, Vm.Entry.Longitude), Distance.FromMiles(.5)));
            // place a pin on the map for the log entry's locations
            _map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = Vm.Entry.Title,
                Position = new Position(Vm.Entry.Latitude, Vm.Entry.Longitude)
            });
        }
    }
}
