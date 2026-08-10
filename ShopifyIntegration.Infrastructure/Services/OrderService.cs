using Microsoft.Extensions.Options;
using ShopifyIntegration.Application.Abstractions;
using ShopifyIntegration.Application.Exceptions;
using ShopifyIntegration.Domain.Entities;
using ShopifyIntegration.Infrastructure.Configuration;
using ShopifyIntegration.Infrastructure.GraphQL.Clients;
using ShopifyIntegration.Infrastructure.GraphQL.Models;
using ShopifyIntegration.Infrastructure.GraphQL.Queries;
using ShopifyIntegration.Infrastructure.Models.GetOrders;

namespace ShopifyIntegration.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IShopifyGraphQlClient _graphQlClient;
        private readonly ShopifyOptions _options;

        public OrderService(IShopifyGraphQlClient graphQlClient, IOptions<ShopifyOptions> options)
        {
            _graphQlClient = graphQlClient;
            _options = options.Value;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync(
            CancellationToken cancellationToken = default
        )
        {
            var variables = new GetProductsVariables(_options.NumberOfOrders);

            var request = new GraphQlRequest<GetProductsVariables>(
                ShopifyQueries.GetOrders,
                variables
            );

            OrdersQueryData data = await _graphQlClient.ExecuteAsync<OrdersQueryData>(
                request,
                cancellationToken
            );
            OrderConnectionResponse? productsResponse = data.Orders;

            if (productsResponse is null)
            {
                throw new ShopifyIntegrationException(
                    "La respuesta no contiene la sección de ordenes."
                );
            }

            return productsResponse
                .Edges.Where(edge => edge.Node is not null)
                .Select(edge =>
                {
                    ShopifyOrderResponse node = edge.Node!;

                    return new Order(
                        node.Id,
                        node.Name,
                        node.Customer.DisplayName,
                        node.CreatedAt,
                        node.DisplayFinancialStatus,
                        node.DisplayFulfillmentStatus,
                        node.TotalPriceSet.ShopMoney.Amount,
                        node.TotalPriceSet.ShopMoney.CurrencyCode
                    );
                })
                .ToList();
        }
    }
}
