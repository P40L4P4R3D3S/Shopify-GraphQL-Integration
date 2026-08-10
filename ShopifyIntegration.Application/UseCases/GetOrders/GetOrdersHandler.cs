using ShopifyIntegration.Application.Abstractions;
using ShopifyIntegration.Application.DTOs;
using ShopifyIntegration.Domain.Entities;

namespace ShopifyIntegration.Application.UseCases.GetOrders;

public sealed class GetOrdersHandler : IGetOrdersHandler
{
    private readonly IOrderService _orderService;

    public GetOrdersHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IReadOnlyList<OrderDto>> HandleAsync(
        CancellationToken cancellationToken = default
    )
    {
        IReadOnlyList<Order> orders = await _orderService.GetOrdersAsync(cancellationToken);

        return orders
            .Select(order => new OrderDto(
                order.Id,
                order.Name,
                order.CustomerName,
                order.CreatedAt,
                order.FinancialStatus,
                order.FulfillmentStatus,
                order.TotalAmount,
                order.CurrencyCode
            ))
            .ToList();
    }
}
