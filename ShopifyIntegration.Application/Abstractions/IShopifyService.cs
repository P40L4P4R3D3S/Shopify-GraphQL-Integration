using ShopifyIntegration.Domain.Entities;

namespace ShopifyIntegration.Application.Abstractions;

public interface IShopifyService
{
    Task<Shop> GetShopAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Product>> GetProductsAsync(CancellationToken cancellationToken = default);
}
