namespace ShopifyIntegration.Domain.Entities;

public sealed class Order
{
    public Order(
        string id,
        string name,
        string customerName,
        string email,
        string createdAt,
        string financialStatus,
        string fulfillmentStatus,
        string totalAmount,
        string currencyCode,
        IReadOnlyList<LineItem> lineItems
    )
    {
        Id = id;
        Name = name;
        CustomerName = customerName;
        Email = email;
        CreatedAt = createdAt;
        FinancialStatus = financialStatus;
        FulfillmentStatus = fulfillmentStatus;
        TotalAmount = totalAmount;
        CurrencyCode = currencyCode;
        LineItems = lineItems;
    }

    public string Id { get; }
    public string Name { get; }
    public string CustomerName { get; }
    public string Email { get; }
    public string CreatedAt { get; }
    public string FinancialStatus { get; }
    public string FulfillmentStatus { get; }
    public string TotalAmount { get; }
    public string CurrencyCode { get; }
    public IReadOnlyList<LineItem> LineItems { get; }
}
