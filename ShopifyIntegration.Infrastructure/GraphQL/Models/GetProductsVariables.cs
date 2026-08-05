using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.GraphQL.Models;

public sealed class GetProductsVariables
{
    public GetProductsVariables(int first)
    {
        First = first;
    }

    [JsonPropertyName("first")]
    public int First { get; }
}
