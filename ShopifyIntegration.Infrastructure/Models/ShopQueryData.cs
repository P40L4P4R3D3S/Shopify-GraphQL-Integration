using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.GraphQL.Models;

internal sealed class ShopQueryData
{
    [JsonPropertyName("shop")]
    public ShopifyShopResponse? Shop { get; init; }
}
