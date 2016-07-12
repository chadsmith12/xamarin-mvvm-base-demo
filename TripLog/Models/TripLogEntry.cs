using System;
namespace TripLog
{
	public class TripLogEntry
	{
		public TripLogEntry()
		{
		}

		public TripLogEntry(string title, double latitude, double longitude, DateTime date, int rating, string notes)
		{
			Title = title;
			Latitude = latitude;
			Longitude = longitude;
			Date = date;
			Rating = rating;
			Notes = notes;
		}

		public string Title { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public DateTime Date { get; set; }
		public int Rating { get; set; }
		public string Notes { get; set; }
	}
}

