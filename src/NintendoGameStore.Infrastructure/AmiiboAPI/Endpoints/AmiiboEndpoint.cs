using Newtonsoft.Json;
using NintendoGameStore.Infrastructure.AmiiboAPI.Interfaces;
using NintendoGameStore.Infrastructure.AmiiboAPI.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NintendoGameStore.Infrastructure.AmiiboAPI.Endpoints
{
    public class AmiiboEndpoint : IAmiiboEndpoint
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string BASE_URL = "https://www.amiiboapi.com/api/amiibo/";

        public AmiiboEndpoint(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<AmiibosJson> GetAmiibosAsync() => (await MakeRequest<AmiibosJson>(BASE_URL));
        public async Task<AmiibosJson> GetAmiibosByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            var url = $"{BASE_URL}?name={name}";
            return await MakeRequest<AmiibosJson>(url);
        }
        private async Task<T> MakeRequest<T>(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            else return default(T);
        }
    }
}
