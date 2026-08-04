using System.Threading;
using System.Threading.Tasks;
using ShopifyIntegration.Application.DTOs;

namespace ShopifyIntegration.Application.UseCases.GetShopInformation;

public interface IGetShopInformationHandler
{
    Task<ShopDto> HandleAsync(CancellationToken cancellationToken = default);
}
