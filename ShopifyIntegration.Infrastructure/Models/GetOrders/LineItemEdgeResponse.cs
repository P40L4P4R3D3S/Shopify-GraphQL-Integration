using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetOrders;

internal sealed class LineItemEdgeResponse
{
    [JsonPropertyName("node")]
    public ShopifyLineItemResponse? Node { get; init; }
}
