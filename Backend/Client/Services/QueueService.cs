using Client.Controllers.V1.AEPS;
using Client.Controllers.V1.VEXS;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;

namespace Client.Services;

public class QueueService : BaseService<Queue, Guid, object>, IQueueService
{
    private readonly IExchangeService _exchangeService;
    private readonly IBlindBoxService _blindBoxService;
    private readonly IUserService _userService;
    private readonly IBaseService<OrdersExchange, Guid, object> _orderExchange;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="exchangeService"></param>
    /// <param name="blindBoxService"></param>
    public QueueService(IBaseRepository<Queue, Guid, object> repository, IExchangeService exchangeService,
        IBlindBoxService blindBoxService, IUserService userService, IBaseService<OrdersExchange, Guid, object> orderExchange) : base(repository)
    {
        _exchangeService = exchangeService;
        _blindBoxService = blindBoxService;
        _userService = userService;
        _orderExchange = orderExchange;
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
                var blindBoxPost = _exchangeService.GetById(request.ExchangeId);
                if (blindBoxPost == null)
                {
                    response.SetMessage(MessageId.I00000, CommonMessages.BlindBoxNotFound);
                    return false;
                }

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
                return true;
            }
        );
        return response;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public VEXSApproveQueueResponse ApproveQueue(VEXSApproveQueueRequest request, IIdentityService identityService)
    {
        var response = new VEXSApproveQueueResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get blindBoxPost
            var blindBoxPost = _exchangeService.GetById(request.ExchangeId);
            if (blindBoxPost == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.BlindBoxNotFound);
                return false;
            }
            var queue = Repository.Find(x => x.QueueId == request.QueueId).FirstOrDefault();
            if (queue == null)
            {
                response.SetMessage(MessageId.I00000);
                return false;
            }
            if (!request.isApprove)
            {
                queue.IsActive = false;
                SaveChanges(userName);
            }
            blindBoxPost.Status = (byte)ConstantEnum.ExchangeStatus.Approve;

            var OrderExchange = new OrdersExchange
            {
                ExchangeId = request.ExchangeId,
                OrderExchangeId = Guid.NewGuid(),
                QueueId = request.QueueId
            };

            _exchangeService.Update(blindBoxPost);
            _exchangeService.SaveChanges(userName);

            _orderExchange.Add(OrderExchange);
            _orderExchange.SaveChanges(userName);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        return response;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public VEXSGetOrderExchangeResponse GetOrderExchange(VEXSGetOrderExchangeRequest request, IIdentityService identityService)
    {
        var response = new VEXSGetOrderExchangeResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get blindBoxPost
            var blindBoxPost = _exchangeService.Find(x => x.Status == (byte)ConstantEnum.ExchangeStatus.Approve, isTracking: false);
            var queue = _orderExchange.Find(x => x.Queue.CreatedBy == userName, isTracking:false, x => x.Exchange).Select(x => new VEXSGetOrderExchangeResponseEntity
            {
                ExchangeId = x.ExchangeId,
                ExchangeName = x.Exchange.ExchangeName
            }).ToList();

            List<VEXSGetOrderExchangeResponseEntity> orderExchange = new List<VEXSGetOrderExchangeResponseEntity>();

            if (blindBoxPost.Any())
            {
                 orderExchange = blindBoxPost.Where(x => x.CreatedBy == userName).Select(x => new VEXSGetOrderExchangeResponseEntity
                {
                    ExchangeId = x.ExchangeId,
                    ExchangeName = x.CreatedBy
                }).ToList();
            }
            else if(queue.Any())
            {
                orderExchange.AddRange(queue);
            }

            response.Response = orderExchange;

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        return response;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public VEXSGetToQueueResponse GetQueue(VEXSGetToQueueRequest request, IIdentityService identityService)
    {
        var response = new VEXSGetToQueueResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            var user = _userService.SelectUserProfile(identityService).Response;

            var queue = Repository.Find(x => x.ExchangeId == request.ExchangeId, isTracking: false);

            if (request.SearchName != null)
            {
                queue = queue.Where(x => x.CreatedBy == request.SearchName);  
            }

            var res = queue.Select(x => new VEXSGetToQueueResponseEntity
            {
                QueueId = x.QueueId,
                DescriptionQueue = x.Description,
                Status = x.Status,
                userBirthday = user.BirthDate.Value,
                userFullNameQueue = user.FirstName,
                UserGender = user.Gender == 0 ? "Male" : "Female" ,
                UserImage = user.ImageUrl,
                UserPhone = user.PhoneNumber
            }).ToList();

            response.Response = res;

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        return response;
    }
}