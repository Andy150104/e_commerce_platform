using Client.Controllers.V1.UDS;
using Client.Controllers.V1.Users;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;

namespace Client.Services;

public class AddressService : BaseService<Address, Guid, VwUserAddress>, IAddressService
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="unitOfWork"></param>
    public AddressService(IBaseRepository<Address, Guid, VwUserAddress> repository) : base(repository)
    {
    }

    /// <summary>
    /// Insert user address
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public UDSInsertUserAddressResponse InsertUserAddress(UDSInsertUserAddressRequest request, IIdentityService identityService)
    {
        var response = new UDSInsertUserAddressResponse() { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
        
        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Insert user address
            var userAddress = new Address
            {
                Username = userName,
                AddressLine = request.AddressLine,
                Ward = request.Ward,
                District = request.District,
                City = request.City,
                Province = request.Province
            };
            Repository.Add(userAddress);
            Repository.SaveChanges(userAddress.Username);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }

    /// <summary>
    /// Update user address
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public UDSUpdateUserAddressResponse UpdateUserAddress(UDSUpdateUserAddressRequest request, IIdentityService identityService)
    {
        var response = new UDSUpdateUserAddressResponse() { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
        
        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get address
            var address = Repository.Find(x => x.Username == userName && x.AddressId == request.AddressId).FirstOrDefault();
        
            // Update address information
            if (request.AddressLine != null)
            {
                address.AddressLine = request.AddressLine;
            }
        
            if (request.Ward != null)
            {
                address.Ward = request.Ward;
            }
        
            if (request.District != null)
            {
                address.District = request.District;
            }
        
            if (request.City != null)
            {
                address.City = request.City;
            }

            if (request.Province != null)
            {
                address.Province = request.Province;
            }
        
            // Save changes
            Repository.Update(address);
            Repository.SaveChanges(address.Username);
        
            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }

    /// <summary>
    /// Delete user address
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public UDSDeleteUserAddressResponse DeleteUserAddress(UDSDeleteUserAddressRequest request, IIdentityService identityService)
    {
        var response = new UDSDeleteUserAddressResponse() { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
        
        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get address
            var address = Repository.Find(x => x.Username == userName && x.AddressId == request.AddressId).FirstOrDefault();
        
            // Delete address
            Repository.Update(address);
            Repository.SaveChanges(userName, true);
        
            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }
}