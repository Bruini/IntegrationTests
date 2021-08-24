using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NintendoGameStore.API;
using NintendoGameStore.IntegrationTests.Fixtures;
using System.IO;
using System.Reflection;
using Xunit;

namespace NintendoGameStore.IntegrationTests.Controllers
{
    //public class CategoryControllerIntegrationTest : IClassFixture<BaseEfRepoTestFixture>
    //{
    //    private const string ApiUrlBase = "api/category";

    //    public TestServer CreateServer()
    //    {
    //        var path = Assembly.GetAssembly(typeof(CategoryControllerIntegrationTest)).Location;

    //        var hostBuilder = new WebHostBuilder()
    //            .UseContentRoot(Path.GetDirectoryName(path))
    //            .UseStartup<Startup>();

    //        return new TestServer(hostBuilder);
    //    }

    //    //[Fact]
    //    //public async Task Get_Should_Returns_Ok()
    //    //{
    //    //    using (var server = this.CreateServer())
    //    //    {
    //    //        var client = server.CreateClient();
    //    //        var response = await client.GetAsync("api/category");

    //    //        Assert.NotNull(response.Content);
    //    //        var actionResult = Assert.IsType<ActionResult<IEnumerable<Category>>>(response);
    //    //        Assert.IsType<OkObjectResult>(actionResult.Result);
    //    //    }
    //    //}
    //}
}
