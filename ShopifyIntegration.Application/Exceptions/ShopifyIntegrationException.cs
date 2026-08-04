using System;

namespace ShopifyIntegration.Application.Exceptions;

public sealed class ShopifyIntegrationException : Exception
{
    public ShopifyIntegrationException(string message)
        : base(message) { }

    public ShopifyIntegrationException(string message, Exception innerException)
        : base(message, innerException) { }
}
