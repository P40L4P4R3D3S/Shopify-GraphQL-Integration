using System;
using System.Threading;
using System.Threading.Tasks;
using ShopifyIntegration.Application.DTOs;
using ShopifyIntegration.Application.Exceptions;
using ShopifyIntegration.Application.UseCases.GetShopInformation;

namespace ShopifyIntegration.Console.Presentation;

public class ConsoleApplication
{
    private readonly IGetShopInformationHandler _handler;

    public ConsoleApplication(IGetShopInformationHandler handler)
    {
        _handler = handler;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            System.Console.WriteLine("Consultando información de Shopify...");

            System.Console.WriteLine();

            ShopDto shop = await _handler.HandleAsync(cancellationToken);

            System.Console.WriteLine("Información de la tienda");

            System.Console.WriteLine("------------------------");

            System.Console.WriteLine($"Nombre:  {shop.Name}");

            System.Console.WriteLine($"Email:   {shop.Email}");

            System.Console.WriteLine($"Dominio: {shop.Domain}");
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
}
