using ShopifyIntegration.Application.DTOs;

namespace ShopifyIntegration.Application.UseCases.GetOrders;

public interface IGetOrdersHandler
{
    Task<IReadOnlyList<OrderDto>> HandleAsync(CancellationToken cancellationToken = default);
}
