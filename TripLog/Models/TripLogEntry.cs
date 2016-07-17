using System;
using Newtonsoft.Json;

namespace TripLog.Models
{
    public class TripLogEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TripLogEntry"/> class.
        /// </summary>
        public TripLogEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TripLogEntry"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="date">The date.</param>
        /// <param name="rating">The rating.</param>
        /// <param name="notes">The notes.</param>
        public TripLogEntry(string title, double latitude, double longitude, DateTime date, int rating, string notes)
        {
            Title = title;
            Latitude = latitude;
            Longitude = longitude;
            Date = date;
            Rating = rating;
            Notes = notes;
        }

        [JsonProperty("id")]
        public string Id { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Notes { get; set; }
    }
}

