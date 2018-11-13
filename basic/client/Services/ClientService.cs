using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace client
{
    public class ClientService : IClientService
    {

        private HttpClient httpClient;

        public ClientService(IOptions<ClientServiceOptions> options, ILoggerFactory loggerFactory)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(options.Value.Url);
        }

        public async Task<string[]> GetSomeData() {
            
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync("/api/sample");

            if (response.IsSuccessStatusCode)
            {
            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<string[]>(json);

            return result;
            }

            throw new ArgumentException("something went wrong");
        }
    }
}