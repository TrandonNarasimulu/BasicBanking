using System.Threading.Tasks;
using Xunit;

namespace BasicBanking.API.IntegrationTests.Controllers.HealthCheckController
{
    public class HealthCheckControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public HealthCheckControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task TestHealthCheckControllerWithStartup_ShouldSucceedAsync()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/HealthCheck");

            response.EnsureSuccessStatusCode();
        }
    }
}
