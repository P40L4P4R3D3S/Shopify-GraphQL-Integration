using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShopifyIntegration.Application.Abstractions;
using ShopifyIntegration.Infrastructure.Configuration;
using ShopifyIntegration.Infrastructure.GraphQL.Clients;
using ShopifyIntegration.Infrastructure.Services;

namespace ShopifyIntegration.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddOptions<ShopifyOptions>()
            .Bind(configuration.GetSection(ShopifyOptions.SectionName))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.StoreDomain),
                "Shopify:StoreDomain es obligatorio."
            )
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.AccessToken),
                "Shopify:AccessToken es obligatorio."
            )
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.ApiVersion),
                "Shopify:ApiVersion es obligatorio."
            )
            .Validate(
                options => options.NumberOfProducts > 0,
                "Shopify:NumberOfProducts debe ser mayor que cero."
            )
            .Validate(
                options => options.NumberOfProducts <= 250,
                "Shopify:NumberOfProducts no puede ser mayor que 250."
            )
            .Validate(
                options => options.NumberOfOrders > 0,
                "Shopify:NumberOfOrders debe ser mayor que cero."
            )
            .Validate(
                options => options.NumberOfOrders <= 25,
                "Shopify:NumberOfOrders no puede ser mayor que 25."
            )
            .ValidateOnStart();

        services.AddHttpClient<IShopifyGraphQlClient, ShopifyGraphQlClient>(
            ConfigureShopifyHttpClient
        );

        services.AddTransient<IShopifyService, ShopifyService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IOrderService, OrderService>();
        return services;
    }

    private static void ConfigureShopifyHttpClient(
        IServiceProvider serviceProvider,
        HttpClient httpClient
    )
    {
        ShopifyOptions options = serviceProvider
            .GetRequiredService<IOptions<ShopifyOptions>>()
            .Value;

        string storeDomain = NormalizeStoreDomain(options.StoreDomain);

        httpClient.BaseAddress = new Uri($"https://{storeDomain}/admin/api/{options.ApiVersion}/");

        httpClient.DefaultRequestHeaders.Add("X-Shopify-Access-Token", options.AccessToken);

        httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
        );

        httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    private static string NormalizeStoreDomain(string storeDomain)
    {
        string normalizedDomain = storeDomain
            .Trim()
            .Replace("https://", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Replace("http://", string.Empty, StringComparison.OrdinalIgnoreCase)
            .TrimEnd('/');

        if (!normalizedDomain.EndsWith(".myshopify.com", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Shopify:StoreDomain debe tener el formato " + "'nombre-tienda.myshopify.com'."
            );
        }

        return normalizedDomain;
    }
}
