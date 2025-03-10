using Client.Controllers.V1.DPS;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;

namespace Client.Services;

/// <summary>
/// WishListService - Service for wish list
/// </summary>
public class WishListService : BaseService<Wishlist, Guid, VwWishListDisplay>, IWishListService
{
    private readonly IBaseService<Image, Guid, VwImageAccessory> _imageService;
    private readonly IBaseService<WishlistItem, Guid, object> _wishListItemService;
    //private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="imageService"></param>
    /// <param name="wishListItemService"></param>
    /// <param name="unitOfWork"></param>
    public WishListService(IBaseRepository<Wishlist, Guid, VwWishListDisplay> repository,
        IBaseService<Image, Guid, VwImageAccessory> imageService,
        IBaseService<WishlistItem, Guid, object> wishListItemService) : base(repository)
    {
        _imageService = imageService;
        _wishListItemService = wishListItemService;
    }

    /// <summary>
    /// Select wish list of user
    /// </summary>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public DPSSelectWishListResponse SelectWishList(IIdentityService identityService)
    {
        var response = new DPSSelectWishListResponse { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Get WishList
        var wishListSelects = Repository
            .FindView(wishList => wishList.CustomerUsername == userName)
            .ToList();
        if (!wishListSelects.Any())
        {
            response.SetMessage(MessageId.I00000, CommonMessages.WishListItemNotFound);
            return response;
        }

        // Set Response
        var wishListResponse = new List<DPSSelectWishListEntity>();
        foreach (var wishList in wishListSelects)
        {
            var imageSelects = _imageService
                .FindView(x => x.AccessoryId == wishList.AccessoryId)
                .Select(x => new DPSSelectWishListImages
                {
                    ImageUrl = x.ImageUrl
                })
                .ToList();

            var wishListEntity = new DPSSelectWishListEntity
            {
                AccessoryId = wishList.AccessoryId,
                AccessoryName = wishList.AccessoryName,
                ShortDescription = wishList.ShortDescription,
                Images = imageSelects,
            };
            wishListResponse.Add(wishListEntity);
        }

        // True
        response.Success = true;
        response.Response = wishListResponse;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Insert wish list
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public DPSInsertWishListResponse InsertWishList(DPSInsertWishListRequest request, IIdentityService identityService)
    {
        var response = new DPSInsertWishListResponse { Success = false };
        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        Repository.ExecuteInTransaction(() =>
        {
            // Get WishList
            var wishListSelect = Repository.Find(x => x.UserName == userName, true).FirstOrDefault();
            if (wishListSelect == null)
            {
                // Create new wishList
                var wishList = new Wishlist
                {
                    UserName = userName,
                };
                Repository.Add(wishList);
                Repository.SaveChanges(userName);
                wishListSelect = wishList;
            }

            // WishListItem Select
            var wishListItem = _wishListItemService
                .Find(item => item.AccessoryId == request.CodeAccessory
                              && item.WishlistId == wishListSelect.WishlistId,
                    true)
                .FirstOrDefault();

            if (wishListItem == null)
            {
                var newWishListItem = new WishlistItem
                {
                    AccessoryId = request.CodeAccessory,
                    WishlistId = wishListSelect.WishlistId,
                };
                _wishListItemService.Add(newWishListItem);
            }
            else
            {
                if (!wishListItem.IsActive)
                {
                    wishListItem.IsActive = true;
                }

                _wishListItemService.Update(wishListItem);
            }

            _wishListItemService.SaveChanges(userName);
        });

        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Delete wish list
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public DPSDeleteWishListResponse DeleteWishList(DPSDeleteWishListRequest request, IIdentityService identityService)
    {
        var response = new DPSDeleteWishListResponse { Success = false };

        // Get UserName
        var userName = identityService.IdentityEntity.UserName;
        
        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get WishList
            var wishListSelect = Repository.Find(x => x.UserName == userName && x.IsActive == true).FirstOrDefault();
            if (wishListSelect == null)
            {
                // Error
                response.SetMessage(MessageId.I00000, CommonMessages.WishListNotFound);
                return;
            }

            // Get WishListItem
            var wishListItemSelect = _wishListItemService
                .Find(x => x.WishlistId == wishListSelect.WishlistId
                           && x.AccessoryId == request.CodeAccessory
                           && x.IsActive == true)
                .FirstOrDefault();

            if (wishListItemSelect == null)
            {
                // Error
                response.SetMessage(MessageId.I00000, CommonMessages.WishListItemNotFound);
                return;
            }

            _wishListItemService.Update(wishListItemSelect);
            _wishListItemService.SaveChanges(userName, true);
            
            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }
}