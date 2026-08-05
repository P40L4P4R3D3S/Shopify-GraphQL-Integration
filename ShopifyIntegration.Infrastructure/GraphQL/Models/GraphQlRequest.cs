using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.GraphQL.Models;

public sealed class GraphQlRequest<TVariables>
{
    public GraphQlRequest(string query, TVariables variables)
    {
        Query = query;
        Variables = variables;
    }

    [JsonPropertyName("query")]
    public string Query { get; }

    [JsonPropertyName("variables")]
    public TVariables Variables { get; }
}

public sealed class GraphQlRequest
{
    public GraphQlRequest(string query)
    {
        Query = query;
    }

    [JsonPropertyName("query")]
    public string Query { get; }
}
