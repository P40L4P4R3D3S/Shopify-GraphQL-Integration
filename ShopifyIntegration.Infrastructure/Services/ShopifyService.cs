using ShopifyIntegration.Application.Abstractions;
using ShopifyIntegration.Application.Exceptions;
using ShopifyIntegration.Domain.Entities;
using ShopifyIntegration.Infrastructure.GraphQL.Clients;
using ShopifyIntegration.Infrastructure.GraphQL.Models;
using ShopifyIntegration.Infrastructure.GraphQL.Queries;
using ShopifyIntegration.Infrastructure.Models.GetShopInformation;

namespace ShopifyIntegration.Infrastructure.Services;

public sealed class ShopifyService : IShopifyService
{
    private readonly IShopifyGraphQlClient _graphQlClient;

    public ShopifyService(IShopifyGraphQlClient graphQlClient)
    {
        _graphQlClient = graphQlClient;
    }

    public async Task<Shop> GetShopAsync(CancellationToken cancellationToken = default)
    {
        var request = new GraphQlRequest(ShopifyQueries.GetShopInformation);

        ShopQueryData data = await _graphQlClient.ExecuteAsync<ShopQueryData>(
            request,
            cancellationToken
        );

        ShopifyShopResponse? shopResponse = data.Shop;

        if (shopResponse is null)
        {
            throw new ShopifyIntegrationException(
                "La respuesta no contiene información de la tienda."
            );
        }

        return new Shop(shopResponse.Name, shopResponse.Email, shopResponse.MyShopifyDomain);
    }
}
