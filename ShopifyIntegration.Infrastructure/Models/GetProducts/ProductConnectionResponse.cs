using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetProducts;

internal sealed class ProductConnectionResponse
{
    [JsonPropertyName("edges")]
    public IReadOnlyList<ProductEdgeResponse> Edges { get; init; } = [];
}
