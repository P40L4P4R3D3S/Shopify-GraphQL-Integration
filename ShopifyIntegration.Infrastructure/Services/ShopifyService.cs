using System.Net.Http.Json;
using System.Text.Json;
using ShopifyIntegration.Application.Abstractions;
using ShopifyIntegration.Application.Exceptions;
using ShopifyIntegration.Domain.Entities;
using ShopifyIntegration.Infrastructure.GraphQL.Models;
using ShopifyIntegration.Infrastructure.GraphQL.Queries;

namespace ShopifyIntegration.Infrastructure.Services;

internal sealed class ShopifyService : IShopifyService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private readonly HttpClient _httpClient;

    public ShopifyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
}
