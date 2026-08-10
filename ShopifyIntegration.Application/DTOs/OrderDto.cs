namespace ShopifyIntegration.Application.DTOs
{
    public sealed record OrderDto(
        string Id,
        string Name,
        string CustomerName,
        string CustomerEmail,
        string CreatedAt,
        string FinancialStatus,
        string FulfillmentStatus,
        string TotalAmount,
        string CurrencyCode,
        IReadOnlyList<LineItemDto> LineItems
    );
}
