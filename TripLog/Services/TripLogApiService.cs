using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TripLog.Interfaces;
using TripLog.Models;
using Xamarin.Forms;

namespace TripLog.Services
{
    public class TripLogApiService : BaseHttpService, ITripLogApiService
    {
        #region Private Fields

        private readonly Uri _baseUrl;
        private readonly IDictionary<string, string> _headers;

        #endregion

        #region Constructors

        public TripLogApiService(Uri baseUrl)
        {
            _baseUrl = baseUrl;
            _headers = new Dictionary<string, string> {{"zumo-api-version", "2.0.0"}};
        }
        #endregion

        #region Public Methods
        public async Task<IList<TripLogEntry>> GetEntriesAsync()
        {
            var url = new Uri(_baseUrl, "/tables/entry");
            var response = await SendRequestAsync<TripLogEntry[]>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<TripLogEntry> GetEntryAsync(string id)
        {
            var url = new Uri(_baseUrl, $"/tables/entry/{id}");
            var response = await SendRequestAsync<TripLogEntry>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<TripLogEntry> SaveEntryAsync(TripLogEntry entry)
        {
            if (String.IsNullOrWhiteSpace(entry.Id))
            {
                var url = new Uri(_baseUrl, "tables/entry");
                var response = await SendRequestAsync<TripLogEntry>(url, HttpMethod.Post, _headers, entry);

                return response;
            }
            else
            {
                var url = new Uri(_baseUrl, $"/tables/entry/{entry.Id}");
                var response = await SendRequestAsync<TripLogEntry>(url, new HttpMethod("PATH"), _headers, entry);

                return response;
            }
        }

        public async Task RemoveEntryAsync(TripLogEntry entry)
        {
            var url = new Uri(_baseUrl, $"/tables/entry/{entry.Id}");
            await SendRequestAsync<TripLogEntry>(url, HttpMethod.Delete, _headers);
        }

        #endregion


    }
}
