using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetOrders
{
    internal class MoneyResponse
    {
        [JsonPropertyName("amount")]
        public string Amount { get; init; } = string.Empty;

        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; init; } = string.Empty;
    }
}
