using Client.Controllers.V1.DPS;
using Client.Controllers.V1.MPS;
using Client.Models;

namespace Client.Services;

public interface IAccessoryService : IBaseService<Accessory, string, VwAccessoryDisplay>
{
    Task<MPSInsertAccessoryResponse> InsertAccessory(MPSInsertAccessoryRequest request, IIdentityService identityService);
    
    Task<MSPInsertImageAccessoryResponse> InsertImageAccessory(MSPInsertImageAccessoryRequest request, IIdentityService identityService);
    
    DPSSelectAccessoryResponse SelectAccessory(DPSSelectAccessoryRequest request);
    
    MPSSelectAccessoriesResponse SelectAccessories(MPSSelectAccessoriesRequest request);

    List<ItemEntity> SelectByAccessory(decimal? minPrice, decimal? maxPrice, string nameAccessory,
        string parentCategory, string childCategory, byte? sortBy, string userName);
    
    Task<MPSUpdateAccessoryResponse> UpdateAccessory(MPSUpdateAccessoryRequest request, IIdentityService identityService);
    
    MPSDeleteAccessoryResponse DeleteAccessory(MPSDeleteAccessoryRequest request, IIdentityService identityService);
    
    MPSDeleteImageAccessoryResponse DeleteImageAccessory(MPSDeleteImageAccessoryRequest request, IIdentityService identityService);
}