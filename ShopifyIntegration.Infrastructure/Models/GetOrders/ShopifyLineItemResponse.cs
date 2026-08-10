using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetOrders;

internal sealed class ShopifyLineItemResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("quantity")]
    public int Quantity { get; init; }

    [JsonPropertyName("originalUnitPriceSet")]
    public required MoneyBagResponse OriginalUnitPriceSet { get; init; }
}
