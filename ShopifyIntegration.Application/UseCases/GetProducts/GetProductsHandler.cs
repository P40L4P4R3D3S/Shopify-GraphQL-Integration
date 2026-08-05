using ShopifyIntegration.Application.Abstractions;
using ShopifyIntegration.Application.DTOs;
using ShopifyIntegration.Domain.Entities;

namespace ShopifyIntegration.Application.UseCases.GetProducts;

public sealed class GetProductsHandler : IGetProductsHandler
{
    private readonly IShopifyService _shopifyService;

    public GetProductsHandler(IShopifyService shopifyService)
    {
        _shopifyService = shopifyService;
    }

    public async Task<IReadOnlyList<ProductDto>> HandleAsync(
        CancellationToken cancellationToken = default
    )
    {
        IReadOnlyList<Product> products = await _shopifyService.GetProductsAsync(cancellationToken);

        return products
            .Select(product => new ProductDto(
                product.Id,
                product.Title,
                product.Vendor,
                product.ProductType,
                product.Status
            ))
            .ToList();
    }
}
