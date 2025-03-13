using Client.Controllers.V1.AEPS;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;

namespace Client.Services;

public class ExchangeService : BaseService<Exchange, Guid, object>, IExchangeService
{
    private readonly IIdentityService _identityService;
    private readonly IBlindBoxService _blindBoxService;
    private readonly IBaseService<ImagesBlindBox, Guid, object> _imagesService;
    private readonly ILogicCommonRepository _logicCommonRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="identityService"></param>
    /// <param name="blindBoxService"></param>
    /// <param name="imagesService"></param>
    /// <param name="logicCommonRepository"></param>
    public ExchangeService(IBaseRepository<Exchange, Guid, object> repository, IIdentityService identityService, 
        IBlindBoxService blindBoxService, IBaseService<ImagesBlindBox, Guid, object> imagesService, 
        ILogicCommonRepository logicCommonRepository) : base(repository)
    {
        _identityService = identityService;
        _blindBoxService = blindBoxService;
        _imagesService = imagesService;
        _logicCommonRepository = logicCommonRepository;
    }

    /// <summary>
    /// Add exchange accessory
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public AEPSAddExchangeAccessoryResponse AddExchangeAccessory(AEPSAddExchangeAccessoryRequest request, IIdentityService identityService)
    {
        var response = new AEPSAddExchangeAccessoryResponse() { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
        
        // Check image
        var checkImage = _logicCommonRepository.ImageCheck(request.ImageUrls).Result;

        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Add new blind box
            var blindBox = new BlindBox
            {
                Username = userName
            };
            _blindBoxService.Add(blindBox);
            _identityService.SaveChanges(userName);
        
            // Add images
            blindBox.ImagesBlindBoxes = request.ImageUrls
                .Select(i => new ImagesBlindBox
                { 
                    BlindBoxId = blindBox.BlindBoxId, 
                    ImageUrl = i 
                }).ToList();

            foreach (var image in blindBox.ImagesBlindBoxes)
            {
                _imagesService.Add(image);
            }
            _identityService.SaveChanges(userName);
        
            // Add exchange with blind box
            var exchange = new Exchange
            {
                BlindBoxId = blindBox.BlindBoxId,
            };

            // Check image
            if (!checkImage)
            {
                exchange.Status = (byte) ConstantEnum.PostingStatus.Fail;
            }
            else
                exchange.Status = (byte) ConstantEnum.PostingStatus.PendingExchange;

            Repository.Add(exchange);
            _identityService.SaveChanges(userName);
            
            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            if (!checkImage)
            {
                response.SetMessage("Invalid Images");
            }

        });
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
            response.SetMessage(MessageId.E00000);
            return response;
        }

        response.Response = exchangeList.Select(exchange => new AEPSGetExchangeAccessoryEntity
        {
            ExchangeId = exchange.ExchangeId,
            BlindBoxId = exchange.BlindBoxId,
            Status = exchange.Status,
            IsActive = exchange.IsActive,
            CreatedAt = exchange.CreatedAt,
            UpdatedAt = exchange.UpdatedAt,
            CreatedBy = exchange.CreatedBy,
            UpdatedBy = exchange.UpdatedBy,
            BlindBox = new AEPSGetExchangeAccessoryBlindBoxEntity
            {
                BlindBoxId = exchange.BlindBox.BlindBoxId,
                Username = exchange.BlindBox.Username,
                CreatedAt = exchange.BlindBox.CreatedAt,
                UpdatedAt = exchange.BlindBox.UpdatedAt,
                CreatedBy = exchange.BlindBox.CreatedBy,
                UpdatedBy = exchange.BlindBox.UpdatedBy,
                IsActive = exchange.BlindBox.IsActive,
                WishlistId = exchange.BlindBox.WishlistId,
                ImagesBlindBoxes = exchange.BlindBox.ImagesBlindBoxes
            .Select(img => new AEPSGetExchangeAccessoryImagesBlindBoxesEntity
            {
                ImageUrl = img.ImageUrl
            })
.ToList()
            }
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
            response.SetMessage(MessageId.E00000);
            return response;
        }

        response.Response = exchangeList.Select(exchange => new AEPSGetFailExchangeAccessoryEntity
        {
            ExchangeId = exchange.ExchangeId,
            BlindBoxId = exchange.BlindBoxId,
            Status = exchange.Status,
            IsActive = exchange.IsActive,
            CreatedAt = exchange.CreatedAt,
            UpdatedAt = exchange.UpdatedAt,
            CreatedBy = exchange.CreatedBy,
            UpdatedBy = exchange.UpdatedBy,
            BlindBox = new AEPSGetFailExchangeAccessoryBlindBoxEntity
            {
                BlindBoxId = exchange.BlindBox.BlindBoxId,
                Username = exchange.BlindBox.Username,
                CreatedAt = exchange.BlindBox.CreatedAt,
                UpdatedAt = exchange.BlindBox.UpdatedAt,
                CreatedBy = exchange.BlindBox.CreatedBy,
                UpdatedBy = exchange.BlindBox.UpdatedBy,
                IsActive = exchange.BlindBox.IsActive,
                WishlistId = exchange.BlindBox.WishlistId,
                ImagesBlindBoxes = exchange.BlindBox.ImagesBlindBoxes
            .Select(img => new AEPSGetFailExchangeAccessoryImagesBlindBoxesEntity
            {
                ImageUrl = img.ImageUrl
            }).ToList()
            }
        }).ToList();


        response.Success = true;
        response.SetMessage(MessageId.I00001);

        return response;
    }
}