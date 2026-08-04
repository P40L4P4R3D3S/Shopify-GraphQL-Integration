using ShopifyIntegration.Application.Abstractions;
using ShopifyIntegration.Application.DTOs;
using ShopifyIntegration.Domain.Entities;

namespace ShopifyIntegration.Application.UseCases.GetShopInformation;

public sealed class GetShopInformationHandler : IGetShopInformationHandler
{
    private readonly IShopifyService _shopifyService;

    public GetShopInformationHandler(IShopifyService shopifyService)
    {
        _shopifyService = shopifyService;
    }

    public async Task<ShopDto> HandleAsync(CancellationToken cancellationToken = default)
    {
        Shop shop = await _shopifyService.GetShopAsync(cancellationToken);

        return new ShopDto(shop.Name, shop.Email, shop.MyShopifyDomain);
    }
}
