using System.Net.Http;
using Dotnet.Testing.Web;
using Dotnet.Testing.Web.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Dotnet.IntegrationTest.Helpers
{
    public class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory<Dotnet.Testing.Web.Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        protected IntegrationTestBase(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            EnsureDatabaseReadyBeforeTests(factory);
        }
        
        internal ApplicationDbContext GetDbContext()
        {
            return _factory.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        internal void EnsureDatabaseReadyBeforeTests(CustomWebApplicationFactory<Startup> factory)
        {
            using var dbContext = factory.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Initialize();
        }
        
        internal HttpClient GetHttpClient()
        {
            return _factory.WithWebHostBuilder(builder =>
                {
                    /*
                    builder.ConfigureServices(services =>
                    {
                        var serviceProvider = services.BuildServiceProvider();

                        using (var scope = serviceProvider.CreateScope())
                        {
                            var scopedServices = scope.ServiceProvider;
                            var db = scopedServices
                                .GetRequiredService<ApplicationDbContext>();
                            var logger = scopedServices
                                .GetRequiredService<ILogger<MessagePageTests>>();

                            try
                            {
                                Utilities.ReinitializeDbForTests(db);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "An error occurred seeding " +
                                                    "the database with test messages. Error: {Message}",
                                    ex.Message);
                            }
                        }
                    });
                    */
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
        }
    }
}