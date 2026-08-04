using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopifyIntegration.Application;
using ShopifyIntegration.Console.Presentation;
using ShopifyIntegration.Infrastructure;

namespace ShopifyIntegration.Console;

public static class Program
{
    public static async Task Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddApplication();

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddTransient<ConsoleApplication>();

        using IHost host = builder.Build();

        ConsoleApplication application = host.Services.GetRequiredService<ConsoleApplication>();

        await application.RunAsync();
    }
}
