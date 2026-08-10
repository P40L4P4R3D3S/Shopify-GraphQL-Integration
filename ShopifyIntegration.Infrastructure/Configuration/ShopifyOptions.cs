namespace ShopifyIntegration.Infrastructure.Configuration;

public sealed class ShopifyOptions
{
    public const string SectionName = "Shopify";

    public string StoreDomain { get; init; } = string.Empty;

    public string AccessToken { get; init; } = string.Empty;

    public string ApiVersion { get; init; } = string.Empty;

    public int NumberOfProducts { get; init; }
    public int NumberOfOrders { get; init; }
}
