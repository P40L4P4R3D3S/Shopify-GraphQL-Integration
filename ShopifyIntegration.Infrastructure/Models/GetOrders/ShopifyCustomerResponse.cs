using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetOrders
{
    internal sealed class ShopifyCustomerResponse
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; init; } = string.Empty;
    }
}
