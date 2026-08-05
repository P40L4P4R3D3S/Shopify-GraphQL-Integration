namespace ShopifyIntegration.Infrastructure.GraphQL.Clients;

public interface IShopifyGraphQlClient
{
    Task<TData> ExecuteAsync<TData>(object request, CancellationToken cancellationToken = default);
}
