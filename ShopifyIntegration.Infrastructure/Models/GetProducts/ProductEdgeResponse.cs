using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetProducts;

internal sealed class ProductEdgeResponse
{
    [JsonPropertyName("node")]
    public ShopifyProductResponse? Node { get; init; }
}
