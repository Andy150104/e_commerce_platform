using Client.Controllers.V1.AEPS;
using Client.Models;

namespace Client.Services;

public interface IExchangeRecheckRequestService : IBaseService<ExchangeRecheckRequest, Guid, object>
{
    AEPSSendExchangeRecheckRequestAccessoryResponse SendReCheck(AEPSSendExchangeRecheckRequestAccessoryRequest request, IIdentityService identityService);
    AEPSGetExchangeRecheckRequestAccessoryResponse GetAllRequest
        (AEPSGetExchangeRecheckRequestAccessoryRequest request, IIdentityService identityService);
    AEPSCheckExchangeRecheckRequestAccessoryResponse CheckExchange(AEPSCheckExchangeRecheckRequestAccessoryRequest request, IIdentityService identityService);
}