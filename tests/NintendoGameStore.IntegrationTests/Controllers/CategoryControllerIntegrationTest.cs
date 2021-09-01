using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using NintendoGameStore.API;
using NintendoGameStore.API.Controllers;
using NintendoGameStore.Core.Models;
using NintendoGameStore.IntegrationTests.Fixtures;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace NintendoGameStore.IntegrationTests.Controllers
{
    //public class CategoryControllerIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    //{
    //    private const string ApiUrlBase = "api/category";
    //    private static HttpClient _httpClientWithFullIntegration;
    //    private readonly WebApplicationFactory<Startup> _webApplicationFactory;
    //    public CategoryControllerIntegrationTest(WebApplicationFactory<Startup> webApplicationFactory)
    //    {
    //        _webApplicationFactory = webApplicationFactory;
    //        _httpClientWithFullIntegration ??= webApplicationFactory.CreateClient();
    //    }
    //    public TestServer CreateServer()
    //    {
    //        var path = Assembly.GetAssembly(typeof(CategoryController)).Location;

    //        var hostBuilder = new WebHostBuilder()
    //            .UseContentRoot(Path.GetDirectoryName(path))
    //            .UseStartup<Startup>();

    //        return new TestServer(hostBuilder);
    //    }

    //    [Fact]
    //    public async Task Get_Should_Returns_Ok()
    //    {
    //        var server = CreateServer();

    //        var response = await _httpClientWithFullIntegration.GetAsync(ApiUrlBase);

    //        Assert.NotNull(response.Content);
    //        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

    //    }
    //}
}
