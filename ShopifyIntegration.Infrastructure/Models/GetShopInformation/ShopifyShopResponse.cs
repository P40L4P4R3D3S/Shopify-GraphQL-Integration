using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetShopInformation;

internal sealed class ShopifyShopResponse
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("myshopifyDomain")]
    public string MyShopifyDomain { get; init; } = string.Empty;
}
