using Microsoft.AspNetCore.Mvc;

namespace DDD.Api.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductController : ControllerBase
{

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("GetProduct");
    }
}
