using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShopifyIntegration.Application.DTOs;

namespace ShopifyIntegration.Console.Presentation
{
    internal static class Presenters
    {
        internal static void DisplayProducts(
            CancellationToken cancellationToken,
            IReadOnlyList<ProductDto> products
        )
        {
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

        internal static void DisplayShopInformation(
            CancellationToken cancellationToken,
            ShopDto shop
        )
        {
            System.Console.WriteLine("Información de la tienda");

            System.Console.WriteLine("------------------------");

            System.Console.WriteLine($"Nombre:  {shop.Name}");

            System.Console.WriteLine($"Email:   {shop.Email}");

            System.Console.WriteLine($"Dominio: {shop.Domain}");
        }

        internal static void DisplayOrderList(string title, IReadOnlyList<OrderDto> orders)
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
                        $"  Precio unitario: "
                            + $"{lineItem.UnitPrice} "
                            + $"{lineItem.CurrencyCode}"
                    );
                }

                System.Console.WriteLine();

                System.Console.WriteLine("================================================");
            }
        }
    }
}
