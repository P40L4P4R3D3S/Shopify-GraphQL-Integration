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

    public const string GetProducts = """
        query GetProducts($first: Int!) {
          products(first: $first) {
            edges {
              node {
                id
                title
                vendor
                productType
                status
              }
            }
          }
        }
        """;
}
