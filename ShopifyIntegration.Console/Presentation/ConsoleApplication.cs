using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShopifyIntegration.Application.DTOs;
using ShopifyIntegration.Application.Exceptions;
using ShopifyIntegration.Application.UseCases.GetProducts;
using ShopifyIntegration.Application.UseCases.GetShopInformation;

namespace ShopifyIntegration.Console.Presentation;

public sealed class ConsoleApplication
{
    private readonly IGetShopInformationHandler _shopHandler;
    private readonly IGetProductsHandler _productsHandler;

    public ConsoleApplication(
        IGetShopInformationHandler shopHandler,
        IGetProductsHandler productsHandler
    )
    {
        _shopHandler = shopHandler;
        _productsHandler = productsHandler;
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
