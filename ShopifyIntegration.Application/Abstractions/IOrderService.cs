using ShopifyIntegration.Domain.Entities;

namespace ShopifyIntegration.Application.Abstractions;

public interface IOrderService
{
    Task<IReadOnlyList<Order>> GetOrdersAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Order>> GetPaidOrdersAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Order>> GetUnfulfilledOrdersAsync(
        CancellationToken cancellationToken = default
    );
}
