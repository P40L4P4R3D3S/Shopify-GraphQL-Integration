using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models;

internal sealed class ProductsQueryData
{
    [JsonPropertyName("products")]
    public ProductConnectionResponse? Products { get; init; }
}
