using Newtonsoft.Json;
using PhotoSearch.Api.Exceptions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PhotoSearch.Api.Infrastructure
{
    public class CustomHttpClient : IHttpClient
    {
        private readonly HttpClient client;

        public CustomHttpClient() {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string requestUri) {
            var response = await client.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode)
                throw new DenialException(HttpStatusCode.BadGateway, $"could not get via {requestUri}");

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}
