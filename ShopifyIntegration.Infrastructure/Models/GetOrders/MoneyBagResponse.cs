using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ShopifyIntegration.Infrastructure.Models.GetOrders
{
    internal sealed class MoneyBagResponse
    {
        [JsonPropertyName("shopMoney")]
        public MoneyResponse ShopMoney { get; init; }
    }
}
