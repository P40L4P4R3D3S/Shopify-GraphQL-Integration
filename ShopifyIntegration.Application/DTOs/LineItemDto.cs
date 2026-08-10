namespace ShopifyIntegration.Application.DTOs;

public sealed record LineItemDto(
    string Id,
    string Name,
    int Quantity,
    string UnitPrice,
    string CurrencyCode
);
