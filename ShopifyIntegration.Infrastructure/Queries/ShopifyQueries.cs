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

    public const string GetOrders = """
        query GetOrders(
          $first: Int!,
          $query: String,
          $lineItemsFirst: Int!
        ) {
          orders(
            first: $first
            query: $query
            sortKey: CREATED_AT
            reverse: true
          ) {
            edges {
              node {
                id
                name
                customer {
                  displayName
                  email
                }
                createdAt
                displayFinancialStatus
                displayFulfillmentStatus
                totalPriceSet {
                  shopMoney {
                    amount
                    currencyCode
                  }
                }
                lineItems(first: $lineItemsFirst) {
                  edges {
                    node {
                      id
                      name
                      quantity
                      sku
                      variantTitle
                      originalUnitPriceSet {
                        shopMoney {
                          amount
                          currencyCode
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
        """;

    public const string PaidOrdersFilter = "financial_status:paid";

    public const string UnfulfilledOrdersFilter = "fulfillment_status:unfulfilled";
}
