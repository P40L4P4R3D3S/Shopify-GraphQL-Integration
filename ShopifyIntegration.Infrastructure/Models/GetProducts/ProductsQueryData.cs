using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetProducts;

internal sealed class ProductsQueryData
{
    [JsonPropertyName("products")]
    public ProductConnectionResponse? Products { get; init; }
}
