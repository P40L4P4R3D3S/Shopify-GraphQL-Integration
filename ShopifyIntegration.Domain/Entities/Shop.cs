namespace ShopifyIntegration.Domain.Entities;

public class Shop
{
    public Shop(string name, string email, string myShopifyDomain)
    {
        Name = name;
        Email = email;
        MyShopifyDomain = myShopifyDomain;
    }

    public string Name { get; }

    public string Email { get; }

    public string MyShopifyDomain { get; }
}
