using Client.Controllers.V1.VEXS;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;

namespace Client.Services;

public class QueueService : BaseService<Queue, Guid, object>, IQueueService
{
    private readonly IExchangeService _exchangeService;
    private readonly IBlindBoxService _blindBoxService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="exchangeService"></param>
    /// <param name="blindBoxService"></param>
    public QueueService(IBaseRepository<Queue, Guid, object> repository, IExchangeService exchangeService,
        IBlindBoxService blindBoxService) : base(repository)
    {
        _exchangeService = exchangeService;
        _blindBoxService = blindBoxService;
    }

    /// <summary>
    /// Add to queue
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public VEXSAddToQueueResponse AddToQueue(VEXSAddToQueueRequest request, IIdentityService identityService)
    {
        var response = new VEXSAddToQueueResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Begin transaction
        Repository.ExecuteInTransaction(() =>
            {
                // Get blindBoxPost
                var blindBoxPost = _exchangeService.GetById(request.BlindBoxId);
                if (blindBoxPost == null)
                {
                    response.SetMessage(MessageId.I00000, CommonMessages.BlindBoxNotFound);
                    return;
                }

                var blindBox = new BlindBox
                {
                    Username = userName,
                };
                _blindBoxService.Add(blindBox);
                var queue = new Queue
                {
                    Description = request.Description,
                    ExchangeId = blindBoxPost.ExchangeId,
                    Status = (byte) ConstantEnum.ExchangeStatus.PendingExchange,
                };
                Repository.Add(queue);

                // Save
                Repository.SaveChanges(userName);

                // True
                response.Success = true;
                response.SetMessage(MessageId.I00001);
            }
        );
        return response;
    }
}