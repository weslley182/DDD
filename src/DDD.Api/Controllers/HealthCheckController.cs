using Microsoft.AspNetCore.Mvc;

namespace DDD.Api.Controllers;

[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class HealthCheckController : ControllerBase
{
    private readonly ILogger<HealthCheckController> _logger;

    public HealthCheckController(ILogger<HealthCheckController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Get()
    {
        _logger.LogInformation("Health check");
        return Ok("Healthy");
    }
}
