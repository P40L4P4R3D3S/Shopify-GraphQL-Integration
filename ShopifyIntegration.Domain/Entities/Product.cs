using System;
using System.Collections.Generic;
using System.Text;

namespace ShopifyIntegration.Domain.Entities
{
    public class Product
    {
        public Product(string id, string title, string vendor, string productType, string status)
        {
            Id = id;
            Title = title;
            Vendor = vendor;
            ProductType = productType;
            Status = status;
        }

        public string Id { get; }

        public string Title { get; }

        public string Vendor { get; }

        public string ProductType { get; }

        public string Status { get; }
    }
}
