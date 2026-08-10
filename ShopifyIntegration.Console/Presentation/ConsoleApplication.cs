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

        DisplayOrderList("Todas las órdenes", orders);
    }

    private async Task DisplayPaidOrdersAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<OrderDto> orders = await _paidOrdersHandler.HandleAsync(cancellationToken);

        DisplayOrderList("Órdenes pagadas", orders);
    }

    private async Task DisplayUnfulfilledOrdersAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<OrderDto> orders = await _unfulfilledOrdersHandler.HandleAsync(
            cancellationToken
        );

        DisplayOrderList("Órdenes no cumplidas", orders);
    }

    private static void DisplayOrderList(string title, IReadOnlyList<OrderDto> orders)
    {
        System.Console.WriteLine(title);

        System.Console.WriteLine("----------------------------------------");

        System.Console.WriteLine($"Cantidad: {orders.Count}");

        if (orders.Count == 0)
        {
            System.Console.WriteLine("No se encontraron órdenes.");

            return;
        }

        for (int index = 0; index < orders.Count; index++)
        {
            OrderDto order = orders[index];

            System.Console.WriteLine();

            System.Console.WriteLine($"Orden {index + 1} - {order.Name}");

            System.Console.WriteLine("------------------------------------------------");

            System.Console.WriteLine($"Id: {order.Id}");

            System.Console.WriteLine($"Cliente: {order.CustomerName}");

            System.Console.WriteLine($"Email: {order.CustomerEmail}");

            System.Console.WriteLine($"Fecha de creación: {order.CreatedAt}");

            System.Console.WriteLine($"Estado de pago: {order.FinancialStatus}");

            System.Console.WriteLine($"Estado de fulfillment: {order.FulfillmentStatus}");

            System.Console.WriteLine($"Total: {order.TotalAmount} {order.CurrencyCode}");

            System.Console.WriteLine();

            System.Console.WriteLine($"Line Items ({order.LineItems.Count})");

            if (order.LineItems.Count == 0)
            {
                System.Console.WriteLine("  No hay productos en esta orden.");

                continue;
            }

            for (int lineIndex = 0; lineIndex < order.LineItems.Count; lineIndex++)
            {
                LineItemDto lineItem = order.LineItems[lineIndex];

                System.Console.WriteLine();

                System.Console.WriteLine($"  Producto {lineIndex + 1}");

                System.Console.WriteLine($"  Nombre: {lineItem.Name}");

                System.Console.WriteLine($"  Cantidad: {lineItem.Quantity}");

                System.Console.WriteLine(
                    $"  Precio unitario: " + $"{lineItem.UnitPrice} " + $"{lineItem.CurrencyCode}"
                );
            }

            System.Console.WriteLine();

            System.Console.WriteLine("================================================");
        }
    }

    private async Task DisplayShopInformationAsync(CancellationToken cancellationToken)
    {
        ShopDto shop = await _shopHandler.HandleAsync(cancellationToken);

        System.Console.WriteLine("Información de la tienda");

        System.Console.WriteLine("------------------------");

        System.Console.WriteLine($"Nombre:  {shop.Name}");

        System.Console.WriteLine($"Email:   {shop.Email}");

        System.Console.WriteLine($"Dominio: {shop.Domain}");
    }

    private async Task DisplayProductsAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<ProductDto> products = await _productsHandler.HandleAsync(cancellationToken);

        System.Console.WriteLine("Productos de la tienda");

        System.Console.WriteLine("------------------------");

        if (products.Count == 0)
        {
            System.Console.WriteLine("No se encontraron productos en la tienda.");

            return;
        }

        for (int index = 0; index < products.Count; index++)
        {
            ProductDto product = products[index];

            System.Console.WriteLine();
            System.Console.WriteLine($"Producto {index + 1}");

            System.Console.WriteLine("----------------------------------------");

            System.Console.WriteLine($"Id:        {product.Id}");

            System.Console.WriteLine($"Título:    {product.Title}");

            System.Console.WriteLine($"Proveedor: {product.Vendor}");

            System.Console.WriteLine($"Tipo:      {product.ProductType}");

            System.Console.WriteLine($"Estado:    {product.Status}");
        }
    }
}
