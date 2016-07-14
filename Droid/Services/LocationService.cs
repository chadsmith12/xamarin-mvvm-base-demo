using System.Threading.Tasks;
using TripLog.Interfaces;
using TripLog.Models;
using Xamarin.Forms;
using Xamarin.Geolocation;

namespace TripLog.Droid.Services
{
    public class LocationService : ILocationService
    {
        public async Task<GeoCoords> GetGeoCoordinatesAsync()
        {
            var locator = new Geolocator(Forms.Context) {DesiredAccuracy = 30};

            var position = await locator.GetPositionAsync(30000);
            var result = new GeoCoords {Latitude = position.Latitude, Longitude = position.Longitude};

            return result;
        }
    }
}