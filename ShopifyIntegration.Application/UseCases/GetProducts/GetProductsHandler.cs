using ShopifyIntegration.Application.Abstractions;
using ShopifyIntegration.Application.DTOs;
using ShopifyIntegration.Domain.Entities;

namespace ShopifyIntegration.Application.UseCases.GetProducts;

public sealed class GetProductsHandler : IGetProductsHandler
{
    private readonly IProductService _productService;

    public GetProductsHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IReadOnlyList<ProductDto>> HandleAsync(
        CancellationToken cancellationToken = default
    )
    {
        IReadOnlyList<Product> products = await _productService.GetProductsAsync(cancellationToken);

        return products
            .Select(product => new ProductDto(
                product.Id,
                product.Title,
                product.Vendor,
                product.ProductType,
                product.Status
            ))
            .OrderBy(product => product.Title)
            .ToList();
    }
}
