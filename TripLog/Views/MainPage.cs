using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TripLog.Views
{
	public class MainPage : ContentPage
	{
		public MainPage()
		{
			Title = "Triplog";

			var items = new List<TripLogEntry>
			{
				new TripLogEntry("Washington Monument", 38.8895, -77.0352, new DateTime(2015, 2, 5), 3, "Amazing"),
				new TripLogEntry("Statue of Liberty", 40.6892, -74.0444, new DateTime(2015, 4, 13), 4, "Inspiring!!"),
				new TripLogEntry("Golden Gate Bridge", 37.8268, -122.4789, new DateTime(2015, 4, 26), 5, "Foggy, but beautiful!!")
			};

			var itemTemplate = new DataTemplate(typeof(TextCell));
			itemTemplate.SetBinding(TextCell.TextProperty, "Title");
			itemTemplate.SetBinding(TextCell.DetailProperty, "Notes");

			// toolbar
			var newButton = new ToolbarItem {Text = "New"};
			newButton.Clicked += (sender, e) =>
			{
				Navigation.PushAsync(new NewEntryPage());
			};
			ToolbarItems.Add(newButton);

			var entries = new ListView
			{
				ItemsSource = items,
				ItemTemplate = itemTemplate
			};

		    entries.ItemTapped += async (sender, e) =>
		    {
		        var item = (TripLogEntry) e.Item;
		        await Navigation.PushAsync(new DetailPage(item));
		    };

			Content = entries;
		}
	}
}


