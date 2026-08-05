namespace ShopifyIntegration.Application.DTOs;

public sealed record ProductDto(
    string Id,
    string Title,
    string Vendor,
    string ProductType,
    string Status
);
