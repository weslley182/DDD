using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using Xunit;

namespace DDD.Integration.Tests.Api.Controllers;

public class HealthCheckControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private const string REQUEST_URI = "api/healthcheck";

    public HealthCheckControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task HealthCheck_ReturnsHealthyStatus()
    {
        // Act
        var response = await _client.GetAsync(REQUEST_URI);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        var deserializedContent = JsonSerializer.Deserialize<string>(content);
        Assert.Equal("Healthy", deserializedContent);
    }

    //[Fact(Skip = "somente teste de exceção")]
    [Fact]
    public async Task HealthCheck_ReturnsExceptionWhenErrorRouteIsAccessed()
    {
        // Arrange
        var requestUri = REQUEST_URI + "/error";

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
        });
    }

    [Fact]
    public async Task HealthCheck_ReturnsExceptionWhenServerIsNotRunning()
    {
        var client = new HttpClient();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            var response = await client.GetAsync(REQUEST_URI);
            response.EnsureSuccessStatusCode();
        });
    }
}