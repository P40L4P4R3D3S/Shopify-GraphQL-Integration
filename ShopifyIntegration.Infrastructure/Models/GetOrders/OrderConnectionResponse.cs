using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetOrders
{
    internal sealed class OrderConnectionResponse
    {
        [JsonPropertyName("edges")]
        public IReadOnlyList<OrderEdgeResponse> Edges { get; init; } = [];
    }
}
