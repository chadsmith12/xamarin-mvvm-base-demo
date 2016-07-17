using System.Collections.Generic;
using System.Threading.Tasks;
using TripLog.Models;

namespace TripLog.Interfaces
{
    /// <summary>
    /// Provides a backend service to get trip log data from an API
    /// </summary>
    public interface ITripLogApiService
    {
        /// <summary>
        /// Gets the entries asynchronous.
        /// </summary>
        /// <returns>List of TripLogEntry</returns>
        Task<IList<TripLogEntry>> GetEntriesAsync();

        /// <summary>
        /// Gets the entry asynchronous.
        /// </summary>
        /// <returns>TripLogEntry</returns>
        Task<TripLogEntry> GetEntryAsync(string id);

        /// <summary>
        /// Saves the entry asynchronous.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns>TripLogEntry</returns>
        Task<TripLogEntry> SaveEntryAsync(TripLogEntry entry);

        /// <summary>
        /// Removes the entry asynchronous.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns>TripLogEntry</returns>
        Task RemoveEntryAsync(TripLogEntry entry);
    }
}
