using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public partial class BasicIntegrationTests
    : IClassFixture<WebApplicationFactory<TimeTracker.Startup>>
    {
        private readonly WebApplicationFactory<TimeTracker.Startup> _factory;

        public BasicIntegrationTests(WebApplicationFactory<TimeTracker.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home")]
        [InlineData("/Home/Privacy")]
        [InlineData("/Home/Error")]
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


        [Theory]
        [InlineData("/Customers")]
        [InlineData("/Customers/Create")]
        [InlineData("/Customers/Update/1")]
        [InlineData("/Invoices")]
        [InlineData("/Invoices/Create")]
        [InlineData("/Invoices/Update/1")]
        [InlineData("/TimeEntries/Create")]
        [InlineData("/TimeEntries/Update/1")]
        public async Task Get_FailAuthorizationRequirement(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/Customers")]
        [InlineData("/Customers/Create")]
        [InlineData("/Customers/Update/1")]
        [InlineData("/Invoices")]
        [InlineData("/Invoices/Create")]
        [InlineData("/Invoices/Update/1")]
        [InlineData("/TimeEntries/Create")]
        [InlineData("/TimeEntries/Update/1")]
        public async Task Get_SecurePageIsReturnedForAnAuthenticatedUser(string url)
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication("TestingAccount")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "TestingAccount", options => { });
                });
            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });

            //Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
