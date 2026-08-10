using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetOrders;

internal sealed class LineItemConnectionResponse
{
    [JsonPropertyName("edges")]
    public IReadOnlyList<LineItemEdgeResponse> Edges { get; init; } = [];
}
