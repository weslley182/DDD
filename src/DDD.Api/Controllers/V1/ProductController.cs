using DDD.Application.UseCase.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Api.Controllers.V1;

[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductUseCase _useCase;
    public ProductController(IProductUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _useCase.GetAllProductsAsyc();
        return Ok("GetProduct");
    }
}
