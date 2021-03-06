using NUnit.Framework;

namespace Dotnet.Testing.IntegrationTestt
{
    
    // https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0
    public class BasicTests 
        : IClassFixture<WebApplicationFactory<RazorPagesProject.Startup>>
    {
        private readonly WebApplicationFactory<RazorPagesProject.Startup> _factory;

        public BasicTests(WebApplicationFactory<RazorPagesProject.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        [InlineData("/About")]
        [InlineData("/Privacy")]
        [InlineData("/Contact")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }
    }
}