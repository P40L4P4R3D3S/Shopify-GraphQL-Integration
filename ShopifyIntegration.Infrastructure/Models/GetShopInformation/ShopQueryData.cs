using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetShopInformation;

internal sealed class ShopQueryData
{
    [JsonPropertyName("shop")]
    public ShopifyShopResponse? Shop { get; init; }
}
