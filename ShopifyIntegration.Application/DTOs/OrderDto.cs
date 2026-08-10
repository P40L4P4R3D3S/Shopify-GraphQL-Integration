namespace ShopifyIntegration.Application.DTOs
{
    public sealed record OrderDto(
        string id,
        string name,
        string customerName,
        string createdAt,
        string financialStatus,
        string fulfillmentStatus,
        string totalAmount,
        string currencyCode
    );
}
