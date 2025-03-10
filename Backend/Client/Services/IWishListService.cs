using Client.Controllers.V1.DPS;
using Client.Models;

namespace Client.Services;

public interface IWishListService : IBaseService<Wishlist, Guid, VwWishListDisplay>
{
    DPSSelectWishListResponse SelectWishList(IIdentityService identityService);
    
    DPSInsertWishListResponse InsertWishList(DPSInsertWishListRequest request, IIdentityService identityService);
    
    DPSDeleteWishListResponse DeleteWishList(DPSDeleteWishListRequest request, IIdentityService identityService);
}