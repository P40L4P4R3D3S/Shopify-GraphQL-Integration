using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ShopifyIntegration.Application.Abstractions;
using ShopifyIntegration.Application.Exceptions;
using ShopifyIntegration.Domain.Entities;
using ShopifyIntegration.Infrastructure.Configuration;
using ShopifyIntegration.Infrastructure.GraphQL.Models;
using ShopifyIntegration.Infrastructure.GraphQL.Queries;
using ShopifyIntegration.Infrastructure.Models;

namespace ShopifyIntegration.Infrastructure.Services;

public sealed class ShopifyService : IShopifyService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private readonly HttpClient _httpClient;
    private readonly ShopifyOptions _options;

    public ShopifyService(HttpClient httpClient, IOptions<ShopifyOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<Shop> GetShopAsync(CancellationToken cancellationToken = default)
    {
        var graphQlRequest = new GraphQlRequest(ShopifyQueries.GetShopInformation);

        using HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync(
            "graphql.json",
            graphQlRequest,
            JsonOptions,
            cancellationToken
        );

        string responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new ShopifyIntegrationException(
                $"Shopify respondió con el código HTTP "
                    + $"{(int)httpResponse.StatusCode} "
                    + $"({httpResponse.StatusCode})."
            );
        }

        GraphQlResponse<ShopQueryData>? graphQlResponse;

        try
        {
            graphQlResponse = JsonSerializer.Deserialize<GraphQlResponse<ShopQueryData>>(
                responseContent,
                JsonOptions
            );
        }
        catch (JsonException exception)
        {
            throw new ShopifyIntegrationException(
                "La respuesta de Shopify no tiene un formato JSON válido.",
                exception
            );
        }

        if (graphQlResponse is null)
        {
            throw new ShopifyIntegrationException("Shopify devolvió una respuesta vacía.");
        }

        if (graphQlResponse.Errors is { Count: > 0 })
        {
            string errorMessages = string.Join(
                Environment.NewLine,
                graphQlResponse.Errors.Select(error => $"- {error.Message}")
            );

            throw new ShopifyIntegrationException(
                $"Shopify devolvió errores GraphQL:" + $"{Environment.NewLine}{errorMessages}"
            );
        }

        ShopifyShopResponse? shopResponse = graphQlResponse.Data?.Shop;

        if (shopResponse is null)
        {
            throw new ShopifyIntegrationException(
                "La respuesta no contiene información de la tienda."
            );
        }

        return new Shop(shopResponse.Name, shopResponse.Email, shopResponse.MyShopifyDomain);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(
        CancellationToken cancellationToken = default
    )
    {
        var variables = new GetProductsVariables(_options.NumberOfProducts);

        var graphQlRequest = new GraphQlRequest<GetProductsVariables>(
            ShopifyQueries.GetProducts,
            variables
        );

        using HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync(
            "graphql.json",
            graphQlRequest,
            JsonOptions,
            cancellationToken
        );

        string responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new ShopifyIntegrationException(
                $"Shopify respondió con el código HTTP "
                    + $"{(int)httpResponse.StatusCode} "
                    + $"({httpResponse.StatusCode})."
            );
        }

        GraphQlResponse<ProductsQueryData>? graphQlResponse;

        try
        {
            graphQlResponse = JsonSerializer.Deserialize<GraphQlResponse<ProductsQueryData>>(
                responseContent,
                JsonOptions
            );
        }
        catch (JsonException exception)
        {
            throw new ShopifyIntegrationException(
                "La respuesta de Shopify no tiene un formato JSON válido.",
                exception
            );
        }

        if (graphQlResponse is null)
        {
            throw new ShopifyIntegrationException("Shopify devolvió una respuesta vacía.");
        }

        if (graphQlResponse.Errors is { Count: > 0 })
        {
            string errorMessages = string.Join(
                Environment.NewLine,
                graphQlResponse.Errors.Select(error => $"- {error.Message}")
            );

            throw new ShopifyIntegrationException(
                $"Shopify devolvió errores GraphQL:" + $"{Environment.NewLine}{errorMessages}"
            );
        }

        ProductConnectionResponse? productsResponse = graphQlResponse.Data?.Products;

        if (productsResponse is null)
        {
            throw new ShopifyIntegrationException(
                "La respuesta no contiene la sección de productos."
            );
        }

        IReadOnlyList<Product> products = productsResponse
            .Edges.Where(edge => edge.Node is not null)
            .Select(edge =>
            {
                ShopifyProductResponse node = edge.Node!;

                return new Product(node.Id, node.Title, node.Vendor, node.ProductType, node.Status);
            })
            .ToList();

        return products;
    }
}
