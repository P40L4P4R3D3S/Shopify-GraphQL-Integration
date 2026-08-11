using Microsoft.Extensions.Options;
using ShopifyIntegration.Application.Exceptions;
using ShopifyIntegration.Domain.Entities;
using ShopifyIntegration.Infrastructure.Configuration;
using ShopifyIntegration.Infrastructure.GraphQL.Clients;
using ShopifyIntegration.Infrastructure.GraphQL.Models;
using ShopifyIntegration.Infrastructure.GraphQL.Queries;
using ShopifyIntegration.Infrastructure.Models.GetOrders;
using ShopifyIntegration.Interfaces.Services;

namespace ShopifyIntegration.Infrastructure.Services;

public sealed class OrderService : IOrderService
{
    private readonly IShopifyGraphQlClient _graphQlClient;
    private readonly ShopifyOptions _options;

    public OrderService(IShopifyGraphQlClient graphQlClient, IOptions<ShopifyOptions> options)
    {
        _graphQlClient = graphQlClient;
        _options = options.Value;
    }

    public Task<IReadOnlyList<Order>> GetOrdersAsync(CancellationToken cancellationToken = default)
    {
        return GetOrdersByFilterAsync(null, cancellationToken);
    }

    public Task<IReadOnlyList<Order>> GetPaidOrdersAsync(
        CancellationToken cancellationToken = default
    )
    {
        return GetOrdersByFilterAsync(ShopifyQueries.PaidOrdersFilter, cancellationToken);
    }

    public Task<IReadOnlyList<Order>> GetUnfulfilledOrdersAsync(
        CancellationToken cancellationToken = default
    )
    {
        return GetOrdersByFilterAsync(ShopifyQueries.UnfulfilledOrdersFilter, cancellationToken);
    }

    private async Task<IReadOnlyList<Order>> GetOrdersByFilterAsync(
        string? filter,
        CancellationToken cancellationToken
    )
    {
        var variables = new GetOrdersVariables(
            _options.NumberOfOrders,
            _options.NumberOfLineItems,
            filter
        );

        var request = new GraphQlRequest<GetOrdersVariables>(ShopifyQueries.GetOrders, variables);

        OrdersQueryData data = await _graphQlClient.ExecuteAsync<OrdersQueryData>(
            request,
            cancellationToken
        );

        OrderConnectionResponse? ordersResponse = data.Orders;

        if (ordersResponse is null)
        {
            throw new ShopifyIntegrationException(
                "La respuesta no contiene la sección de órdenes."
            );
        }

        return ordersResponse
            .Edges.Where(edge => edge.Node is not null)
            .Select(edge =>
            {
                ShopifyOrderResponse node = edge.Node!;

                IReadOnlyList<LineItem> lineItems = node
                    .LineItems.Edges.Where(lineItemEdge => lineItemEdge.Node is not null)
                    .Select(lineItemEdge =>
                    {
                        ShopifyLineItemResponse lineItemNode = lineItemEdge.Node!;

                        return new LineItem(
                            lineItemNode.Id,
                            lineItemNode.Name,
                            lineItemNode.Quantity,
                            lineItemNode.OriginalUnitPriceSet.ShopMoney.Amount,
                            lineItemNode.OriginalUnitPriceSet.ShopMoney.CurrencyCode
                        );
                    })
                    .ToList();

                return new Order(
                    node.Id,
                    node.Name,
                    node.Customer?.DisplayName ?? "Cliente no disponible",
                    node.Customer?.Email ?? "Email no disponible",
                    node.CreatedAt,
                    node.DisplayFinancialStatus,
                    node.DisplayFulfillmentStatus,
                    node.TotalPriceSet.ShopMoney.Amount,
                    node.TotalPriceSet.ShopMoney.CurrencyCode,
                    lineItems
                );
            })
            .ToList();
    }
}
