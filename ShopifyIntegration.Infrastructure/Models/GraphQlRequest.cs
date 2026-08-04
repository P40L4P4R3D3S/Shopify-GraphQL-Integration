using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.GraphQL.Models;

internal sealed class GraphQlRequest
{
    public GraphQlRequest(string query)
    {
        Query = query;
    }

    [JsonPropertyName("query")]
    public string Query { get; }
}
