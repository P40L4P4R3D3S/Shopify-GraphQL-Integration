namespace ShopifyIntegration.Infrastructure.GraphQL.Queries;

internal static class ShopifyQueries
{
    public const string GetShopInformation = """
        query GetShopInformation {
          shop {
            name
            email
            myshopifyDomain
          }
        }
        """;
}
