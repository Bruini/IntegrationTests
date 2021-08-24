using Newtonsoft.Json;
using NintendoGameStore.Infrastructure.AmiiboAPI.Interfaces;
using NintendoGameStore.Infrastructure.AmiiboAPI.Models;
using System;
using System.Collections.Generic;
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
        public async Task<AmiibosJson> GetAmiibosAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, BASE_URL);

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<AmiibosJson>(result);
                return obj;
            }
            else return default(AmiibosJson);
        }

        public async Task<AmiibosJson> GetAmiibosByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{BASE_URL}?name={name}");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AmiibosJson>(result);
            }
            else return default(AmiibosJson);
        }
    }
}
