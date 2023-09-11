using DDD.Application.Dtos;

namespace DDD.Application.UseCase.Interface;

public interface IProductUseCase
{
    Task AddProductAsync(ProductRequest request);
    Task<IEnumerable<ProductResponse>> GetAllProductsAsyc();
}
