using Client.Controllers.V1.AEPS;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;
using System.Linq.Expressions;

namespace Client.Services;

public class ExchangeRecheckRequestService : BaseService<ExchangeRecheckRequest, Guid, object>, IExchangeRecheckRequestService
{
    private readonly IIdentityService _identityService;
    private readonly IExchangeService _exchangeService;
    private readonly ILogicCommonRepository _logicCommonRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="identityService"></param>
    /// <param name="blindBoxService"></param>
    /// <param name="logicCommonRepository"></param>
    public ExchangeRecheckRequestService(IBaseRepository<ExchangeRecheckRequest, Guid, object> repository, IIdentityService identityService, IExchangeService exchangeService,
        ILogicCommonRepository logicCommonRepository) : base(repository)
    {
        _identityService = identityService;
        _logicCommonRepository = logicCommonRepository;
        _exchangeService = exchangeService;
    }

    /// <summary>
    /// Check Exchange
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public AEPSCheckExchangeRecheckRequestAccessoryResponse CheckExchange(AEPSCheckExchangeRecheckRequestAccessoryRequest request, IIdentityService identityService)
    {
        var response = new AEPSCheckExchangeRecheckRequestAccessoryResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        Repository.ExecuteInTransaction(() =>
        {
            var requestExchange = Repository.Find(x => x.RequestId == request.RequestId).FirstOrDefault();

            requestExchange.Status = request.isAccepted ? (byte)1 : (byte)0;

            Repository.Update(requestExchange);
            Repository.SaveChanges(userName);

            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }

    /// <summary>
    /// Get All Request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public AEPSGetExchangeRecheckRequestAccessoryResponse GetAllRequest(AEPSGetExchangeRecheckRequestAccessoryRequest request, IIdentityService identityService)
    {
        var response = new AEPSGetExchangeRecheckRequestAccessoryResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        var exchangeList = Repository
            .Find(isTracking: false)
            .ToList();

        if (!exchangeList.Any())
        {
            response.SetMessage(MessageId.E00000);
            return response;
        }

        response.Response = exchangeList.Select(exchange => new AEPSGetExchangeRecheckRequestAccessoryEntity
        {
            RequestId = exchange.RequestId,
            ExchangeId = exchange.ExchangeId,
            Description = exchange.Description,
            Status = exchange.Status,
            IsActive = exchange.IsActive,
            CreatedAt = exchange.CreatedAt,
            UpdatedAt = exchange.UpdatedAt,
            CreatedBy = exchange.CreatedBy,
            UpdatedBy = exchange.UpdatedBy
        }).ToList();

        response.Success = true;
        response.SetMessage(MessageId.I00001);

        return response;
    }

    /// <summary>
    /// Send Re-check request accessory
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public AEPSSendExchangeRecheckRequestAccessoryResponse SendReCheck(AEPSSendExchangeRecheckRequestAccessoryRequest request, IIdentityService identityService)
    {
        var response = new AEPSSendExchangeRecheckRequestAccessoryResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        Repository.ExecuteInTransaction(() =>
        {

            var exchange = _exchangeService.GetFailExchangeAccessory(new AEPSGetFailExchangeAccessoryRequest (), identityService).Response.FirstOrDefault(x => x.ExchangeId == request.ExchangeId);

            var exchangeReCheckRequest = new ExchangeRecheckRequest
            {
                RequestId = Guid.NewGuid(),
                ExchangeId = request.ExchangeId,
                Description = request.Description,
                Status = (byte)ConstantEnum.RecheckStatus.Pending
            };

            Repository.Add(exchangeReCheckRequest);
            Repository.SaveChanges(userName);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }
}