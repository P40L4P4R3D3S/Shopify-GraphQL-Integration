using ShopifyIntegration.Application.DTOs;
using ShopifyIntegration.Domain.Entities;

namespace ShopifyIntegration.Application.Mappings;

internal static class OrderMapper
{
    public static OrderDto ToDto(Order order)
    {
        IReadOnlyList<LineItemDto> lineItems = order
            .LineItems.Select(lineItem => new LineItemDto(
                lineItem.Id,
                lineItem.Name,
                lineItem.Quantity,
                lineItem.UnitPrice,
                lineItem.CurrencyCode
            ))
            .ToList();

        return new OrderDto(
            order.Id,
            order.Name,
            order.CustomerName,
            order.Email,
            order.CreatedAt,
            order.FinancialStatus,
            order.FulfillmentStatus,
            order.TotalAmount,
            order.CurrencyCode,
            lineItems
        );
    }
}
