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
    private readonly IBlindBoxService _blindBoxService;
    private readonly IBaseService<ImagesBlindBox, Guid, object> _imagesService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="identityService"></param>
    /// <param name="blindBoxService"></param>
    /// <param name="logicCommonRepository"></param>
    public ExchangeRecheckRequestService(IBaseRepository<ExchangeRecheckRequest, Guid, object> repository, IIdentityService identityService, IExchangeService exchangeService,
        ILogicCommonRepository logicCommonRepository, IBlindBoxService blindBoxService, IBaseService<ImagesBlindBox, Guid, object> imagesService) : base(repository)
    {
        _identityService = identityService;
        _logicCommonRepository = logicCommonRepository;
        _exchangeService = exchangeService;
        _blindBoxService = blindBoxService;
        _imagesService = imagesService;
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
            var exchange = _exchangeService.Find(x => x.ExchangeId == requestExchange.ExchangeId).FirstOrDefault();
            var blindBox = _blindBoxService.Find(x => x.BlindBoxId == exchange.BlindBoxId).FirstOrDefault();
            var imageList = _imagesService.Find(x => x.BlindBoxId == blindBox.BlindBoxId).ToList();

            requestExchange.Status = request.isAccepted ? (byte)ConstantEnum.RecheckStatus.Approved : (byte)ConstantEnum.RecheckStatus.Rejected;
            if (requestExchange.Status == (byte)ConstantEnum.RecheckStatus.Approved)
            {
                exchange.Status = (byte)ConstantEnum.ExchangeStatus.PendingExchange;
                _exchangeService.Update(exchange);
                _exchangeService.SaveChanges(userName);
            }

            Repository.Update(requestExchange);
            Repository.SaveChanges(userName);

            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
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

            var exist = Repository.Find(x => x.CreatedBy == userName && x.ExchangeId == request.ExchangeId).FirstOrDefault();
            if(exist != null)
            {
                response.SetMessage(MessageId.E00000, CommonMessages.ReCheckExist);
                return false;
            }

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
            return true;
        });
        return response;
    }
}