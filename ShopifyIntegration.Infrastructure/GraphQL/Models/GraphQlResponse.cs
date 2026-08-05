using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.GraphQL.Models;

public sealed class GraphQlResponse<TData>
{
    [JsonPropertyName("data")]
    public TData? Data { get; init; }

    [JsonPropertyName("errors")]
    public IReadOnlyList<GraphQlErrorResponse>? Errors { get; init; }
}
