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

        // Take note that this test might need to be run on its own. If it fails you can run it again on its own.
        [Fact]
        public async Task TestHealthCheckControllerWithStartup_ShouldSucceedAsync()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/HealthCheck/IsAlive");

            response.EnsureSuccessStatusCode();
        }
    }
}
