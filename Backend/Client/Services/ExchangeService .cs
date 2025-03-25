using Client.Controllers.V1.AEPS;
using Client.Controllers.V1.DPS;
using Client.Controllers.V1.Exchanges;
using Client.Logics.Commons;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Client.Services;

public class ExchangeService : BaseService<Exchange, Guid, VwBlindBoxDisplay>, IExchangeService
{
    private readonly IIdentityService _identityService;
    private readonly IBlindBoxService _blindBoxService;
    private readonly IBaseService<ImagesBlindBox, Guid, VwImageBlindBox> _imagesService;
    private readonly IBaseService<Queue, Guid, object> _queueService;
    private readonly ILogicCommonRepository _logicCommonRepository;
    private readonly IBaseService<OrdersExchange, Guid, object> _orderExchangeService;
    private readonly CloudinaryLogic _cloudinaryLogic;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="identityService"></param>
    /// <param name="blindBoxService"></param>
    /// <param name="imagesService"></param>
    /// <param name="logicCommonRepository"></param>
    public ExchangeService(IBaseRepository<Exchange, Guid, VwBlindBoxDisplay> repository,
        IIdentityService identityService,
        IBlindBoxService blindBoxService, IBaseService<ImagesBlindBox, Guid, VwImageBlindBox> imagesService,
        ILogicCommonRepository logicCommonRepository, CloudinaryLogic cloudinaryLogic, IBaseService<Queue, Guid, object> queueService, IBaseService<OrdersExchange, Guid, object> orderExchange) : base(repository)
    {
        _identityService = identityService;
        _blindBoxService = blindBoxService;
        _imagesService = imagesService;
        _logicCommonRepository = logicCommonRepository;
        _cloudinaryLogic = cloudinaryLogic;
        _queueService = queueService;
        _orderExchangeService = orderExchange;
    }

    /// <summary>
    /// Add exchange accessory
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public async Task<AEPSAddExchangeAccessoryResponse> AddExchangeAccessory(AEPSAddExchangeAccessoryRequest request, IIdentityService identityService)
    {
        var response = new AEPSAddExchangeAccessoryResponse() { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Begin transaction
        await Repository.ExecuteInTransactionAsync(async () =>
        {
            // Add new blind box
            var blindBox = new BlindBox
            {
                BlindBoxId = Guid.NewGuid(),
                Username = userName
            };
            _blindBoxService.Add(blindBox);
            _identityService.SaveChanges(userName);

            // Upload Images in Parallel
            var uploadTasks = request.Images.Select(async image =>
            {
                var imageUrl = await _cloudinaryLogic.UploadImageAsync(image);
                return new ImagesBlindBox
                {
                    ImageId = Guid.NewGuid(),
                    ImageUrl = imageUrl,
                    BlindBoxId = blindBox.BlindBoxId
                };
            }).ToList();
            var images = await Task.WhenAll(uploadTasks);

            // Insert Images
            foreach (var image in images)
            {
                _imagesService.Add(image);
            }
            _identityService.SaveChanges(userName);

            var list = images.Select(i => i.ImageUrl).ToList();

            // Check image
            var checkImage = _logicCommonRepository.ImageCheck(list).Result;
        
            // Add exchange with blind box
            var exchange = new Exchange
            {
                BlindBoxId = blindBox.BlindBoxId,
                Description = request.Description,
                ExchangeName = request.Name
            };

            // Check image
            if (!checkImage)
            {
                exchange.Status = (byte) ConstantEnum.ExchangeStatus.Fail;
            }
            else
                exchange.Status = (byte) ConstantEnum.ExchangeStatus.PendingExchange;

            Repository.Add(exchange);
            _identityService.SaveChanges(userName);
            
            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            if (!checkImage)
            {
                response.SetMessage("Invalid Images");
            }

            return true;
        });
        return response;
    }

    /// <summary>
    /// Final Accepted Exchange
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public AEPSFinalAcceptExchangeAccessoryResponse FinalAcceptedExchange(AEPSFinalAcceptExchangeAccessoryRequest request, IIdentityService identityService)
    {
        var response = new AEPSFinalAcceptExchangeAccessoryResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        var exchange = Repository.Find(x => x.ExchangeId == request.ExchangeId).FirstOrDefault();

        if(exchange == null)
        {
            response.SetMessage(MessageId.E00000, CommonMessages.ExchangeNotFound);
            return response;
        }

        if (exchange.Status != (byte)ConstantEnum.ExchangeStatus.isChanging)
        {
            response.SetMessage(MessageId.E00000, CommonMessages.ExchangeIsNotChanging);
            return response;
        }

        var queue = _queueService.Find(x => x.QueueId == request.QueueId).FirstOrDefault();

        if (queue == null)
        {
            response.SetMessage(MessageId.E00000, CommonMessages.QueueNotFound);
            return response;
        }

        if (queue.Status != (byte)ConstantEnum.QueueStatus.isChanging)
        {
            response.SetMessage(MessageId.E00000, CommonMessages.QueueIsNotChanging);
            return response;
        }

        if (request.isAccepted)
        {
            exchange.Status = (byte)ConstantEnum.ExchangeStatus.Success;
            
            var OrderExchange = new OrdersExchange
            {
                ExchangeId = request.ExchangeId,
                OrderExchangeId = Guid.NewGuid(),
                QueueId = request.QueueId
            };

            _orderExchangeService.Add(OrderExchange);
            _orderExchangeService.SaveChanges(userName);

        }else if(!request.isAccepted)
        {
            exchange.Status = (byte)ConstantEnum.ExchangeStatus.PendingExchange;
            queue.Status = (byte)ConstantEnum.QueueStatus.Fail;
            
           _queueService.Update(queue);
           _queueService.SaveChanges(userName);
        }
        Repository.Update(exchange);
        Repository.SaveChanges(userName);

        return response;
    }

    /// <summary>
    /// Get By Id Exchange
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public AEPSGetByIdExchangeAccessoryResponse GetByIdExchangeAccessory(AEPSGetByIdExchangeAccessoryRequest request, IIdentityService identityService)
    {
        var response = new AEPSGetByIdExchangeAccessoryResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        var exchange = Repository.Find(x => x.ExchangeId == request.ExchangeId, isTracking: false, x => x.BlindBox.ImagesBlindBoxes).FirstOrDefault();

        response.Response =  new AEPSGetByIdExchangeAccessoryEntity
        {
            ExchangeId = exchange.ExchangeId,
            BlindBoxId = exchange.BlindBox.BlindBoxId,
            Status = exchange.Status,
            Description = exchange.Description,
            ExchangeName = exchange.ExchangeName,
            imageBlindBoxList = exchange.BlindBox.ImagesBlindBoxes
    .Select(img => new AEPSGetByIdExchangeAccessoryImageList
    {
        ImageId = img.ImageId,
        ImageUrls = img.ImageUrl
    }).ToList()
        };

        response.Success = true;
        response.SetMessage(MessageId.I00001);

        return response;
    }

    /// <summary>
    /// Get exchange accessory
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public AEPSGetExchangeAccessoryResponse GetExchangeAccessory(AEPSGetExchangeAccessoryRequest request, IIdentityService identityService)
    {
        var response = new AEPSGetExchangeAccessoryResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        var exchangeList = Repository
            .Find(x => x.BlindBox.Username == userName, isTracking: false, x => x.BlindBox, x => x.BlindBox.ImagesBlindBoxes)
            .Where(x => x.Status != (byte)ConstantEnum.ExchangeStatus.Fail)
            .OrderByDescending(x => x.CreatedAt)
            .ToList();

        if (!exchangeList.Any())
        {
            response.SetMessage(MessageId.E00000, CommonMessages.ListIsEmpty);
            return response;
        }

        response.Response = exchangeList.Select(exchange => new AEPSGetExchangeAccessoryEntity
        {
            ExchangeId = exchange.ExchangeId,
            BlindBoxId = exchange.BlindBox.BlindBoxId,
            Status = exchange.Status,
            Description = exchange.Description,
            ExchangeName = exchange.ExchangeName,
            imageBlindBoxList = exchange.BlindBox.ImagesBlindBoxes
            .Select(img => new AEPSGetExchangeAccessoryImageBlindBoxList
            {
                ImageId = img.ImageId,
                ImageUrls = img.ImageUrl
            }).ToList()
        }).ToList();

        response.Success = true;
        response.SetMessage(MessageId.I00001);

        return response;
    }

    /// <summary>
    /// Get Fail Exchange Accessory
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public AEPSGetFailExchangeAccessoryResponse GetFailExchangeAccessory(AEPSGetFailExchangeAccessoryRequest request, IIdentityService identityService)
    {
        var response = new AEPSGetFailExchangeAccessoryResponse() { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        var exchangeList = Repository
            .Find(x => x.BlindBox.Username == userName, isTracking: false, x => x.BlindBox, x => x.BlindBox.ImagesBlindBoxes)
            .Where(x => x.Status == (byte)ConstantEnum.ExchangeStatus.Fail)
            .OrderByDescending(x => x.CreatedAt)
            .ToList();

        if (!exchangeList.Any())
        {
            response.SetMessage(MessageId.I00001);
            return response;
        }

        response.Response = exchangeList.Select(exchange => new AEPSGetFailExchangeAccessoryEntity
        {
            ExchangeId = exchange.ExchangeId,
            BlindBoxId = exchange.BlindBox.BlindBoxId,
            Status = exchange.Status,
            Description = exchange.Description,
            ExchangeName = exchange.ExchangeName,
            imageBlindBoxList = exchange.BlindBox.ImagesBlindBoxes
            .Select(img => new AEPSGetFailExchangeAccessoryImageBlindBoxList
            {
                ImageId = img.ImageId,
                ImageUrls = img.ImageUrl
            }).ToList()
        }).ToList();


        response.Success = true;
        response.SetMessage(MessageId.I00001);

        return response;
    }

    /// <summary>
    /// Select by blind box
    /// </summary>
    /// <param name="sortBy"></param>
    /// <returns></returns>
    public List<ItemEntity> SelectByBlindBox(byte? sortBy)
    {
        var responseEntity = new List<ItemEntity>();

        // Get blind boxes
        var blindBoxs = _blindBoxService.FindView(x => x.Status == (byte)ConstantEnum.PostingStatus.PendingExchange);

        if (sortBy == (byte)ConstantEnum.Sort.Newest || sortBy == (byte)ConstantEnum.Sort.Oldest)
        {
            blindBoxs = CommonLogic.ApplySorting(blindBoxs, sortBy);
        }

        var blindBoxList = blindBoxs.ToList();
        foreach (var blindBox in blindBoxList)
        {
            // Get image urls
            var imageUrls = _imagesService
                .FindView(x => x.BlindBoxId == blindBox!.BlindBoxId)
                .Select(x => new DpsSelectItemListImageUrl
                {
                    ImageUrl = x!.ImageUrl
                })
                .ToList();

            // Create entity
            var entity = new ItemEntity
            {
                CodeProduct = blindBox!.BlindBoxId.ToString(),
                ExchangeName = blindBox.ExchangeName,
                CreatedAt = blindBox.CreatedAt,
                FirstNameCreator = blindBox.FirstName!,
                LastNameCreator = blindBox.LastName!,
                ImageUrl = imageUrls,
                ExchangeId = blindBox.ExchangeId,
            };
            responseEntity.Add(entity);
        }

        return responseEntity;
    }
}