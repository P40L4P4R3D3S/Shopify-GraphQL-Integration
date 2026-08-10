using ShopifyIntegration.Application.DTOs;

namespace ShopifyIntegration.Application.UseCases.GetUnfulfilledOrders;

public interface IGetUnfulfilledOrdersHandler
{
    Task<IReadOnlyList<OrderDto>> HandleAsync(CancellationToken cancellationToken = default);
}
