using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.GraphQL.Models;

public sealed class GetOrdersVariables
{
    public GetOrdersVariables(int first, int lineItemsFirst, string? query = null)
    {
        First = first;
        LineItemsFirst = lineItemsFirst;
        Query = query;
    }

    [JsonPropertyName("first")]
    public int First { get; }

    [JsonPropertyName("lineItemsFirst")]
    public int LineItemsFirst { get; }

    [JsonPropertyName("query")]
    public string? Query { get; }
}
