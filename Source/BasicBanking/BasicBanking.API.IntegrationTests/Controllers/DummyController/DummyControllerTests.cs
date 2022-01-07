using System.Threading.Tasks;
using Xunit;

namespace BasicBanking.API.IntegrationTests.Controllers.DummyController
{
    public class DummyControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DummyControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task TestDummyControllerGetDummyData_ShouldSucceedAsync()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/Dummy/GetDummyData/sampletext");

            response.EnsureSuccessStatusCode();
        }
    }
}
