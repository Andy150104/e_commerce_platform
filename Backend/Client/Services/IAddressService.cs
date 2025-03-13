using Client.Controllers.V1.UDS;
using Client.Controllers.V1.Users;
using Client.Models;

namespace Client.Services;

public interface IAddressService : IBaseService<Address, Guid, VwUserAddress>
{
    UDSInsertUserAddressResponse InsertUserAddress(UDSInsertUserAddressRequest request, IIdentityService identityService);
    
    UDSUpdateUserAddressResponse UpdateUserAddress(UDSUpdateUserAddressRequest request, IIdentityService identityService);
    
    UDSDeleteUserAddressResponse DeleteUserAddress(UDSDeleteUserAddressRequest request, IIdentityService identityService);
}