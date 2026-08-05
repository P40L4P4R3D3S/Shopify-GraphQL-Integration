using System.Net.Http.Json;
using System.Text.Json;
using ShopifyIntegration.Application.Exceptions;
using ShopifyIntegration.Infrastructure.GraphQL.Models;

namespace ShopifyIntegration.Infrastructure.GraphQL.Clients;

internal sealed class ShopifyGraphQlClient : IShopifyGraphQlClient
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private readonly HttpClient _httpClient;

    public ShopifyGraphQlClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TData> ExecuteAsync<TData>(
        object request,
        CancellationToken cancellationToken = default
    )
    {
        using HttpResponseMessage httpResponse = await SendRequestAsync(request, cancellationToken);

        string responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        ValidateHttpResponse(httpResponse, responseContent);

        GraphQlResponse<TData> graphQlResponse = DeserializeResponse<TData>(responseContent);

        ValidateGraphQlErrors(graphQlResponse);

        if (graphQlResponse.Data is null)
        {
            throw new ShopifyIntegrationException(
                "La respuesta GraphQL no contiene la propiedad data."
            );
        }

        return graphQlResponse.Data;
    }

    private async Task<HttpResponseMessage> SendRequestAsync(
        object request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return await _httpClient.PostAsJsonAsync(
                "graphql.json",
                request,
                JsonOptions,
                cancellationToken
            );
        }
        catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw new ShopifyIntegrationException(
                "La solicitud a Shopify excedió el tiempo de espera.",
                exception
            );
        }
        catch (HttpRequestException exception)
        {
            throw new ShopifyIntegrationException(
                "No fue posible establecer comunicación con Shopify.",
                exception
            );
        }
    }

    private static void ValidateHttpResponse(
        HttpResponseMessage httpResponse,
        string responseContent
    )
    {
        if (httpResponse.IsSuccessStatusCode)
        {
            return;
        }

        string message =
            $"Shopify respondió con el código HTTP "
            + $"{(int)httpResponse.StatusCode} "
            + $"({httpResponse.StatusCode}).";

        if (!string.IsNullOrWhiteSpace(responseContent))
        {
            message += $"{Environment.NewLine}" + $"Respuesta: {responseContent}";
        }

        throw new ShopifyIntegrationException(message);
    }

    private static GraphQlResponse<TData> DeserializeResponse<TData>(string responseContent)
    {
        if (string.IsNullOrWhiteSpace(responseContent))
        {
            throw new ShopifyIntegrationException("Shopify devolvió una respuesta vacía.");
        }

        try
        {
            GraphQlResponse<TData>? response = JsonSerializer.Deserialize<GraphQlResponse<TData>>(
                responseContent,
                JsonOptions
            );

            return response
                ?? throw new ShopifyIntegrationException(
                    "No fue posible deserializar la respuesta de Shopify."
                );
        }
        catch (JsonException exception)
        {
            throw new ShopifyIntegrationException(
                "La respuesta de Shopify no tiene un formato JSON válido.",
                exception
            );
        }
    }

    private static void ValidateGraphQlErrors<TData>(GraphQlResponse<TData> graphQlResponse)
    {
        if (graphQlResponse.Errors is not { Count: > 0 })
        {
            return;
        }

        string errorMessages = string.Join(
            Environment.NewLine,
            graphQlResponse.Errors.Select(error => $"- {error.Message}")
        );

        throw new ShopifyIntegrationException(
            $"Shopify devolvió errores GraphQL:" + $"{Environment.NewLine}{errorMessages}"
        );
    }
}
