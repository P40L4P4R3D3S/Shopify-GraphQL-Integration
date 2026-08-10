namespace ShopifyIntegration.Domain.Entities
{
    public class Order
    {
        public Order(
            string id,
            string name,
            string customerName,
            string createdAt,
            string financialStatus,
            string fulfillmentStatus,
            string totalAmount,
            string currencyCode
        )
        {
            Id = id;
            Name = name;
            CustomerName = customerName;
            CreatedAt = createdAt;
            FinancialStatus = financialStatus;
            FulfillmentStatus = fulfillmentStatus;
            TotalAmount = totalAmount;
            CurrencyCode = currencyCode;
        }

        public string Id { get; }
        public string Name { get; }
        public string CustomerName { get; }
        public string CreatedAt { get; }
        public string FinancialStatus { get; }
        public string FulfillmentStatus { get; }
        public string TotalAmount { get; }
        public string CurrencyCode { get; }
    }
}
