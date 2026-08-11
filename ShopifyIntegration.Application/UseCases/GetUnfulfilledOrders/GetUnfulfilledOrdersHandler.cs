using ShopifyIntegration.Application.DTOs;
using ShopifyIntegration.Application.Mappings;
using ShopifyIntegration.Domain.Entities;
using ShopifyIntegration.Interfaces.Services;

namespace ShopifyIntegration.Application.UseCases.GetUnfulfilledOrders;

public sealed class GetUnfulfilledOrdersHandler : IGetUnfulfilledOrdersHandler
{
    private readonly IOrderService _orderService;

    public GetUnfulfilledOrdersHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IReadOnlyList<OrderDto>> HandleAsync(
        CancellationToken cancellationToken = default
    )
    {
        IReadOnlyList<Order> orders = await _orderService.GetUnfulfilledOrdersAsync(
            cancellationToken
        );

        return orders.Select(OrderMapper.ToDto).ToList();
    }
}
