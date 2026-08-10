using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetOrders
{
    internal class ShopifyOrderResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("customer")]
        public ShopifyCustomerResponse? Customer { get; init; }

        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; init; } = string.Empty;

        [JsonPropertyName("displayFinancialStatus")]
        public string DisplayFinancialStatus { get; init; } = string.Empty;

        [JsonPropertyName("displayFulfillmentStatus")]
        public string DisplayFulfillmentStatus { get; init; } = string.Empty;

        [JsonPropertyName("totalPriceSet")]
        public required MoneyBagResponse TotalPriceSet { get; init; }

        [JsonPropertyName("lineItems")]
        public required LineItemConnectionResponse LineItems { get; init; }
    }
}
