using Microsoft.Extensions.DependencyInjection;
using ShopifyIntegration.Application.UseCases.GetShopInformation;

namespace ShopifyIntegration.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IGetShopInformationHandler, GetShopInformationHandler>();

        return services;
    }
}
