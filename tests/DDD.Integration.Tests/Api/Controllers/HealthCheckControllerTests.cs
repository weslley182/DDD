using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using Xunit;

namespace DDD.Integration.Tests.Api.Controllers;

public class HealthCheckControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HealthCheckControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task HealthCheck_ReturnsHealthyStatus()
    {
        // Arrange
        var requestUri = "api/healthcheck";

        // Act
        var response = await _client.GetAsync(requestUri);

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
        var requestUri = "api/healthcheck/error";

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
        // Arrange
        var requestUri = "api/healthcheck"; // Suponhamos que esta seja a rota que você deseja testar.
        var client = new HttpClient();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
        });
    }
}