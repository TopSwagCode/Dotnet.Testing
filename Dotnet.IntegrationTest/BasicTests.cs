using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Dotnet.IntegrationTest
{
    // https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0
    public class BasicTests 
        : IClassFixture<WebApplicationFactory<Dotnet.Testing.Web.Startup>>
    {
        private readonly WebApplicationFactory<Dotnet.Testing.Web.Startup> _factory;

        public BasicTests(WebApplicationFactory<Dotnet.Testing.Web.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        [InlineData("/Privacy")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",response.Content.Headers.ContentType.ToString());
        }
    }
}