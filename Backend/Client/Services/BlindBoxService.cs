using Client.Models;
using Client.Repositories;

namespace Client.Services;

public class BlindBoxService : BaseService<BlindBox, Guid, VwBlindBoxDisplay>, IBlindBoxService
{
    
    // /// <summary>
    // /// Constructor
    // /// </summary>
    // /// <param name="repository"></param>
    // /// <param name="imageBlindBoxService"></param>
    // public BlindBoxService(IBaseRepository<BlindBox, Guid, VwBlindBoxDisplay> repository, IBaseService<ImagesBlindBox, Guid, VwImageBlindBox> imageBlindBoxService, IExchangeService exchangeService) : base(repository)
    // {
    //     _imageBlindBoxService = imageBlindBoxService;
    //     _exchangeService = exchangeService;
    // }
    
    /// <summary>
    /// Select by blind box
    /// </summary>
    /// <param name="sortBy"></param>
    /// <returns></returns>
    // public List<ItemEntity> SelectByBlindBox(byte? sortBy)
    // {
    //     var responseEntity = new List<ItemEntity>();
    //     
    //     // Select Exchanges
    //     var exchanges = _exchangeService
    //         .FindView(x => x.Status == (byte) ConstantEnum.ExchangeStatus.PendingExchange)
    //         .Select(x => x.BlindBoxId)
    //         .ToList();
    //     
    //     var blindBoxDisplays = Repository.FindView(x => exchanges.Contains(x.ExchangeId.Value));
    //
    //     if (sortBy == (byte)ConstantEnum.Sort.Newest || sortBy == (byte)ConstantEnum.Sort.Oldest)
    //     {
    //         blindBoxDisplays = CommonLogic.ApplySorting(blindBoxDisplays, sortBy);
    //     }
    //
    //     var blindBoxList = blindBoxDisplays.ToList();
    //     foreach (var blindBox in blindBoxList)
    //     {
    //         // Get image urls
    //         var imageUrls = _imageBlindBoxService
    //             .FindView(x => x.ImageId == blindBox.BlindBoxId)
    //             .Select(x => new DpsSelectItemListImageUrl
    //             {
    //                 ImageUrl = x.ImageUrl
    //             })
    //             .ToList();
    //         
    //         // Create entity
    //         var entity = new ItemEntity
    //         {
    //             CodeProduct = blindBox.BlindBoxId.ToString(),
    //             CreatedAt = blindBox.CreatedAt,
    //             FirstNameCreator = blindBox.FirstName,
    //             LastNameCreator = blindBox.LastName,
    //             ImageUrl = imageUrls,
    //             ExchangeId = blindBox.ExchangeId,
    //         };
    //         responseEntity.Add(entity);
    //     }
    //
    //     return responseEntity;
    // }
    public BlindBoxService(IBaseRepository<BlindBox, Guid, VwBlindBoxDisplay> repository) : base(repository)
    {
    }
}