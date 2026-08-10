namespace ShopifyIntegration.Domain.Entities;

public sealed class LineItem
{
    public LineItem(string id, string name, int quantity, string unitPrice, string currencyCode)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        UnitPrice = unitPrice;
        CurrencyCode = currencyCode;
    }

    public string Id { get; }

    public string Name { get; }

    public int Quantity { get; }

    public string UnitPrice { get; }

    public string CurrencyCode { get; }
}
