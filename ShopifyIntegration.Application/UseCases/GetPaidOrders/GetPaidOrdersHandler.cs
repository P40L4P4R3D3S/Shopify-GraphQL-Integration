using ShopifyIntegration.Application.DTOs;
using ShopifyIntegration.Application.Mappings;
using ShopifyIntegration.Domain.Entities;
using ShopifyIntegration.Interfaces.Services;

namespace ShopifyIntegration.Application.UseCases.GetPaidOrders;

public sealed class GetPaidOrdersHandler : IGetPaidOrdersHandler
{
    private readonly IOrderService _orderService;

    public GetPaidOrdersHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IReadOnlyList<OrderDto>> HandleAsync(
        CancellationToken cancellationToken = default
    )
    {
        IReadOnlyList<Order> orders = await _orderService.GetPaidOrdersAsync(cancellationToken);

        return orders.Select(OrderMapper.ToDto).ToList();
    }
}
