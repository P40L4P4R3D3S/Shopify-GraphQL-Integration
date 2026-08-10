using Microsoft.Extensions.DependencyInjection;
using ShopifyIntegration.Application.UseCases.GetOrders;
using ShopifyIntegration.Application.UseCases.GetPaidOrders;
using ShopifyIntegration.Application.UseCases.GetProducts;
using ShopifyIntegration.Application.UseCases.GetShopInformation;
using ShopifyIntegration.Application.UseCases.GetUnfulfilledOrders;

namespace ShopifyIntegration.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IGetShopInformationHandler, GetShopInformationHandler>();
        services.AddTransient<IGetProductsHandler, GetProductsHandler>();
        services.AddTransient<IGetOrdersHandler, GetOrdersHandler>();
        services.AddTransient<IGetPaidOrdersHandler, GetPaidOrdersHandler>();
        services.AddTransient<IGetUnfulfilledOrdersHandler, GetUnfulfilledOrdersHandler>();

        return services;
    }
}
