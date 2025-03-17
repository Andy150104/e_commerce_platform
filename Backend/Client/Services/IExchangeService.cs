using Client.Controllers.V1.AEPS;
using Client.Controllers.V1.DPS;
using Client.Models;

namespace Client.Services;

public interface IExchangeService : IBaseService<Exchange, Guid, VwBlindBoxDisplay>
{
    AEPSAddExchangeAccessoryResponse AddExchangeAccessory(AEPSAddExchangeAccessoryRequest request, IIdentityService identityService);
    AEPSGetExchangeAccessoryResponse GetExchangeAccessory(AEPSGetExchangeAccessoryRequest request, IIdentityService identityService);
    AEPSGetFailExchangeAccessoryResponse GetFailExchangeAccessory(AEPSGetFailExchangeAccessoryRequest request, IIdentityService identityService);
    List<ItemEntity> SelectByBlindBox(byte? sortBy);
}