using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShopifyIntegration.Application.DTOs;
using ShopifyIntegration.Application.Exceptions;
using ShopifyIntegration.Application.UseCases.GetOrders;
using ShopifyIntegration.Application.UseCases.GetPaidOrders;
using ShopifyIntegration.Application.UseCases.GetProducts;
using ShopifyIntegration.Application.UseCases.GetShopInformation;
using ShopifyIntegration.Application.UseCases.GetUnfulfilledOrders;

namespace ShopifyIntegration.Console.Presentation;

public sealed class ConsoleApplication
{
    private readonly IGetShopInformationHandler _shopHandler;
    private readonly IGetProductsHandler _productsHandler;
    private readonly IGetOrdersHandler _ordersHandler;
    private readonly IGetPaidOrdersHandler _paidOrdersHandler;
    private readonly IGetUnfulfilledOrdersHandler _unfulfilledOrdersHandler;

    public ConsoleApplication(
        IGetShopInformationHandler shopHandler,
        IGetProductsHandler productsHandler,
        IGetOrdersHandler ordersHandler,
        IGetPaidOrdersHandler paidOrdersHandler,
        IGetUnfulfilledOrdersHandler unfulfilledOrdersHandler
    )
    {
        _shopHandler = shopHandler;
        _productsHandler = productsHandler;
        _ordersHandler = ordersHandler;
        _paidOrdersHandler = paidOrdersHandler;
        _unfulfilledOrdersHandler = unfulfilledOrdersHandler;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            System.Console.WriteLine("Consultando información de Shopify...");

            System.Console.WriteLine();

            await DisplayShopInformationAsync(cancellationToken);

            System.Console.WriteLine();

            await DisplayProductsAsync(cancellationToken);

            System.Console.WriteLine();

            await DisplayOrdersAsync(cancellationToken);

            System.Console.WriteLine();

            await DisplayPaidOrdersAsync(cancellationToken);

            System.Console.WriteLine();

            await DisplayUnfulfilledOrdersAsync(cancellationToken);
        }
        catch (ShopifyIntegrationException exception)
        {
            System.Console.Error.WriteLine("Error al consultar Shopify:");

            System.Console.Error.WriteLine(exception.Message);
        }
        catch (OperationCanceledException)
        {
            System.Console.Error.WriteLine("La operación fue cancelada.");
        }
        catch (Exception exception)
        {
            System.Console.Error.WriteLine("Ocurrió un error inesperado:");

            System.Console.Error.WriteLine(exception.Message);
        }
    }

    private async Task DisplayOrdersAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<OrderDto> orders = await _ordersHandler.HandleAsync(cancellationToken);

        Presenters.DisplayOrderList("Todas las órdenes", orders);
    }

    private async Task DisplayPaidOrdersAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<OrderDto> orders = await _paidOrdersHandler.HandleAsync(cancellationToken);

        Presenters.DisplayOrderList("Órdenes pagadas", orders);
    }

    private async Task DisplayUnfulfilledOrdersAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<OrderDto> orders = await _unfulfilledOrdersHandler.HandleAsync(
            cancellationToken
        );

        Presenters.DisplayOrderList("Órdenes no cumplidas", orders);
    }

    private async Task DisplayShopInformationAsync(CancellationToken cancellationToken)
    {
        ShopDto shop = await _shopHandler.HandleAsync(cancellationToken);

        Presenters.DisplayShopInformation(cancellationToken, shop);
    }

    private async Task DisplayProductsAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<ProductDto> products = await _productsHandler.HandleAsync(cancellationToken);

        Presenters.DisplayProducts(cancellationToken, products);
    }
}
