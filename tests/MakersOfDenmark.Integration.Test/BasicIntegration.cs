using FluentAssertions;
using MakersOfDenmark.WebAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MakersOfDenmark.Integration.Test
{
    public class BasicIntegration
    {
        [Theory]
        [InlineData("/makerspace/")]
        public async Task Endpoint(string endpoint)
        {
            // Arrange
            var factory = new WebApplicationFactory<Startup>();

            // Create a HttpClient which is setup for the test host
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync(endpoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }
    }
}
