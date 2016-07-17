using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TripLog.Services
{
    public abstract class BaseHttpService
    {
        protected async Task<T> SendRequestAsync<T>(Uri url, HttpMethod httpMethod = null, IDictionary<string, string> headers = null, object requestData = null)
        {
            // set to the generically typed parameter
            var result = default(T);
            // default to get
            var method = httpMethod ?? HttpMethod.Get;

            // serialize the request
            var data = requestData == null ? null : JsonConvert.SerializeObject(requestData);

            using (var request = new HttpRequestMessage(method, url))
            {
                // add the request data to the request
                if (data != null)
                {
                    request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                }

                // if we need to add any headers
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                using (var clientHandler = new HttpClientHandler())
                {
                    using (var client = new HttpClient(clientHandler))
                    {
                        using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                        {
                            var content = response.Content == null ? null : await response.Content.ReadAsStringAsync();

                            // response was successful
                            if (response.IsSuccessStatusCode)
                                result = JsonConvert.DeserializeObject<T>(content);
                        }
                    }
                }
            }

            return result;
        }
    }
}
