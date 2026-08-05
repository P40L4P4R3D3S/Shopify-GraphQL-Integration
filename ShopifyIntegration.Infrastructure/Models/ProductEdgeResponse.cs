using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models;

internal sealed class ProductEdgeResponse
{
    [JsonPropertyName("node")]
    public ShopifyProductResponse? Node { get; init; }
}
