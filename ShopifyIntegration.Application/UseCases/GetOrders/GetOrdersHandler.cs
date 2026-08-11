using ShopifyIntegration.Application.DTOs;
using ShopifyIntegration.Application.Mappings;
using ShopifyIntegration.Domain.Entities;
using ShopifyIntegration.Interfaces.Services;

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

        return orders.Select(OrderMapper.ToDto).ToList();
    }
}
