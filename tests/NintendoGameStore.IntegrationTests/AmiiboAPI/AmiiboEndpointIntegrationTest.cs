using Microsoft.Extensions.DependencyInjection;
using NintendoGameStore.Infrastructure.AmiiboAPI.Endpoints;
using NintendoGameStore.Infrastructure.AmiiboAPI.Interfaces;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NintendoGameStore.IntegrationTests.AmiiboAPI
{
    public class AmiiboEndpointIntegrationTest
    {
        private IAmiiboEndpoint _amiiboEndpoint;
        private IHttpClientFactory _httpClientFactory;
        private ServiceCollection _services;
        private ServiceProvider _provider;
        public AmiiboEndpointIntegrationTest()
        {
            _services = new ServiceCollection();
            _services.AddHttpClient();
            _provider = _services.BuildServiceProvider();
        }

        [Fact]
        public async Task GetAmiibosAsync_Should_Return_Itens()
        {
            _httpClientFactory = (IHttpClientFactory)_provider.GetService(typeof(IHttpClientFactory));
            _amiiboEndpoint = new AmiiboEndpoint(_httpClientFactory);

            var result = await _amiiboEndpoint.GetAmiibosAsync();

            Assert.NotNull(result.Amiibo);
            Assert.True(result.Amiibo.Any());
        }

        [Fact]
        public async Task GetAmiibosByNameAsync_Should_Return_One_Iten()
        {
            _httpClientFactory = (IHttpClientFactory)_provider.GetService(typeof(IHttpClientFactory));
            _amiiboEndpoint = new AmiiboEndpoint(_httpClientFactory);

            var result = await _amiiboEndpoint.GetAmiibosByNameAsync("Toon Zelda - The Wind Waker");

            Assert.NotNull(result.Amiibo);
            Assert.Single(result.Amiibo);
        }

        [Fact]
        public async Task GetAmiibosByNameAsync_Should_Return_None()
        {
            _httpClientFactory = (IHttpClientFactory)_provider.GetService(typeof(IHttpClientFactory));
            _amiiboEndpoint = new AmiiboEndpoint(_httpClientFactory);

            var result = await _amiiboEndpoint.GetAmiibosByNameAsync("AAAAAAAAA");

            Assert.Null(result);
        }
    }
}
