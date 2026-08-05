using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetProducts
{
    internal class ShopifyProductResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; init; } = string.Empty;

        [JsonPropertyName("vendor")]
        public string Vendor { get; init; } = string.Empty;

        [JsonPropertyName("productType")]
        public string ProductType { get; init; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; init; } = string.Empty;
    }
}
