using Microsoft.Extensions.Options;
using ShopifyIntegration.Application.Abstractions;
using ShopifyIntegration.Application.Exceptions;
using ShopifyIntegration.Domain.Entities;
using ShopifyIntegration.Infrastructure.Configuration;
using ShopifyIntegration.Infrastructure.GraphQL.Clients;
using ShopifyIntegration.Infrastructure.GraphQL.Models;
using ShopifyIntegration.Infrastructure.GraphQL.Queries;
using ShopifyIntegration.Infrastructure.Models.GetProducts;

namespace ShopifyIntegration.Infrastructure.Services;

public sealed class ProductService : IProductService
{
    private readonly IShopifyGraphQlClient _graphQlClient;
    private readonly ShopifyOptions _options;

    public ProductService(IShopifyGraphQlClient graphQlClient, IOptions<ShopifyOptions> options)
    {
        _graphQlClient = graphQlClient;
        _options = options.Value;
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(
        CancellationToken cancellationToken = default
    )
    {
        var variables = new GetProductsVariables(_options.NumberOfProducts);

        var request = new GraphQlRequest<GetProductsVariables>(
            ShopifyQueries.GetProducts,
            variables
        );

        ProductsQueryData data = await _graphQlClient.ExecuteAsync<ProductsQueryData>(
            request,
            cancellationToken
        );

        ProductConnectionResponse? productsResponse = data.Products;

        if (productsResponse is null)
        {
            throw new ShopifyIntegrationException(
                "La respuesta no contiene la sección de productos."
            );
        }

        return productsResponse
            .Edges.Where(edge => edge.Node is not null)
            .Select(edge =>
            {
                ShopifyProductResponse node = edge.Node!;

                return new Product(node.Id, node.Title, node.Vendor, node.ProductType, node.Status);
            })
            .ToList();
    }
}
