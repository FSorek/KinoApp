using System.Net.Http;
using System.Threading.Tasks;

namespace Kina.Mobile.Core.Services
{
    class HttpService : IHttpService
    {
        private HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> Get(string uri)
        {
            var responseMessage = await _httpClient.GetAsync(uri);
            responseMessage.EnsureSuccessStatusCode();
            var contentAsString = await responseMessage.Content.ReadAsStringAsync();
            return contentAsString;
        }

        public async Task<bool> Post(string uri, StringContent content)
        {
            var responseMessage = await _httpClient.PostAsync(uri, content);
            return responseMessage.IsSuccessStatusCode;
        }
    }
}
