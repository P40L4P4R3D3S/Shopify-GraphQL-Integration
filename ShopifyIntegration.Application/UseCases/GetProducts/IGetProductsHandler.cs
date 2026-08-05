using ShopifyIntegration.Application.DTOs;

namespace ShopifyIntegration.Application.UseCases.GetProducts;

public interface IGetProductsHandler
{
    Task<IReadOnlyList<ProductDto>> HandleAsync(CancellationToken cancellationToken = default);
}
