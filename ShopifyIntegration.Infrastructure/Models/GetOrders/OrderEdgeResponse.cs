using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetOrders
{
    internal sealed class OrderEdgeResponse
    {
        [JsonPropertyName("node")]
        public ShopifyOrderResponse? Node { get; init; }
    }
}
