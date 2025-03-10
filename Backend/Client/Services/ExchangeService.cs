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
                BlindBoxId = blindBox.BlindBoxId
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
            response.SetMessage(MessageId.I00003);
            if (!checkImage)
            {
                response.SetMessage("Invalid Images");
            }

        });
        return response;
    }
}