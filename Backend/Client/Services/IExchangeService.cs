using Client.Controllers.V1.AEPS;
using Client.Models;

namespace Client.Services;

public interface IExchangeService : IBaseService<Exchange, Guid, object>
{
    AEPSAddExchangeAccessoryResponse AddExchangeAccessory(AEPSAddExchangeAccessoryRequest request, IIdentityService identityService);
}