using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.GraphQL.Models;

public sealed class GraphQlErrorResponse
{
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;
}
