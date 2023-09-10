using DDD.Application.Dtos;

namespace DDD.Application.Services.Interfaces;

public interface IProductService
{
    List<ProductResponse> GetAllAsync();
}
