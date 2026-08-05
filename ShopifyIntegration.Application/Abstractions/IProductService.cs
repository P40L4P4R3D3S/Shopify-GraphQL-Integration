using ShopifyIntegration.Domain.Entities;

namespace ShopifyIntegration.Application.Abstractions;

public interface IProductService
{
    Task<IReadOnlyList<Product>> GetProductsAsync(CancellationToken cancellationToken = default);
}
