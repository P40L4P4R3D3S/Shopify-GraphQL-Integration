using Microsoft.Extensions.DependencyInjection;
using ShopifyIntegration.Application.UseCases.GetOrders;
using ShopifyIntegration.Application.UseCases.GetProducts;
using ShopifyIntegration.Application.UseCases.GetShopInformation;

namespace ShopifyIntegration.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IGetShopInformationHandler, GetShopInformationHandler>();
        services.AddTransient<IGetProductsHandler, GetProductsHandler>();
        services.AddTransient<IGetOrdersHandler, GetOrdersHandler>();

        return services;
    }
}
