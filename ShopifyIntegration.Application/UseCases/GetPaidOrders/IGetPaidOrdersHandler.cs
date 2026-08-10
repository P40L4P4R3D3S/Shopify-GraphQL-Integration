using ShopifyIntegration.Application.DTOs;

namespace ShopifyIntegration.Application.UseCases.GetPaidOrders;

public interface IGetPaidOrdersHandler
{
    Task<IReadOnlyList<OrderDto>> HandleAsync(CancellationToken cancellationToken = default);
}
