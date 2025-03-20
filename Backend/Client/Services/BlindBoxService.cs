
using Client.Controllers.V1.DPS;
using Client.Logics.Commons;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;

namespace Client.Services;

public class BlindBoxService : BaseService<BlindBox, Guid, VwBlindBoxDisplay>, IBlindBoxService
{
    private readonly IBaseService<ImagesBlindBox, Guid, VwImageBlindBox> _imageBlindBoxService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="imageBlindBoxService"></param>
    public BlindBoxService(IBaseRepository<BlindBox, Guid, VwBlindBoxDisplay> repository, IBaseService<ImagesBlindBox, Guid, VwImageBlindBox> imageBlindBoxService) : base(repository)
    {
        _imageBlindBoxService = imageBlindBoxService;
    }

    /// <summary>
    /// Select by blind box
    /// </summary>
    /// <param name="sortBy"></param>
    /// <returns></returns>
    public List<ItemEntity> SelectByBlindBox(byte? sortBy)
    {
        var responseEntity = new List<ItemEntity>();

        // Select Accessories
        var blindBoxDisplays = Repository.FindView();
        if (sortBy == (byte)ConstantEnum.Sort.Newest || sortBy == (byte)ConstantEnum.Sort.Oldest)
        {
            blindBoxDisplays = CommonLogic.ApplySorting(blindBoxDisplays, sortBy);
        }

        var blindBixList = blindBoxDisplays.ToList();
        foreach (var blindBox in blindBixList)
        {
            // Get image urls
            var imageUrls = _imageBlindBoxService
                .FindView(x => x.ImageId == blindBox.BlindBoxId)
                .Select(x => new DpsSelectItemListImageUrl
                {
                    ImageUrl = x.ImageUrl
                })
                .ToList();

            // Create entity
            var entity = new ItemEntity
            {
                CodeProduct = blindBox.BlindBoxId.ToString(),
                CreatedAt = blindBox.CreatedAt,
                FirstNameCreator = blindBox.FirstName,
                LastNameCreator = blindBox.LastName,
                ImageUrl = imageUrls,
                ExchangeId = blindBox.ExchangeId,
            };
            responseEntity.Add(entity);
        }

        return responseEntity;
    }
}
