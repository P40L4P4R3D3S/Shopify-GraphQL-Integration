using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShopifyIntegration.Application.Abstractions;
using ShopifyIntegration.Infrastructure.Configuration;
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
            .ValidateOnStart();

        services.AddHttpClient<IShopifyService, ShopifyService>(
            static (serviceProvider, httpClient) =>
            {
                ShopifyOptions options = serviceProvider
                    .GetRequiredService<IOptions<ShopifyOptions>>()
                    .Value;

                string storeDomain = NormalizeStoreDomain(options.StoreDomain);

                httpClient.BaseAddress = new Uri(
                    $"https://{storeDomain}/admin/api/" + $"{options.ApiVersion}/"
                );

                httpClient.DefaultRequestHeaders.Add("X-Shopify-Access-Token", options.AccessToken);

                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                httpClient.Timeout = TimeSpan.FromSeconds(30);
            }
        );

        return services;
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
