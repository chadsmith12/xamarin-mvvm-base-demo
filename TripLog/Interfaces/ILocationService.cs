using System.Threading.Tasks;
using TripLog.Models;

namespace TripLog.Interfaces
{
    public interface ILocationService
    {
        Task<GeoCoords> GetGeoCoordinatesAsync();
    }
}
