using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetOrders
{
    internal sealed class OrdersQueryData
    {
        [JsonPropertyName("orders")]
        public OrderConnectionResponse? Orders { get; init; }
    }
}
