using Client.Controllers.V1.UDS;
using Client.Controllers.V1.UserRegisterScreen;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;

namespace Client.Services;

public class UserService : BaseService<User, string, VwUserProfile>, IUserService
{
    private readonly IIdentityRepository _identityRepository;
    private readonly IAddressService _addressService;
    private readonly ILogicCommonRepository _logicCommonRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="identityRepository"></param>
    /// <param name="addressService"></param>
    /// <param name="logicCommonRepository"></param>
    public UserService(IBaseRepository<User, string, VwUserProfile> repository, IIdentityRepository identityRepository, IAddressService addressService, ILogicCommonRepository logicCommonRepository) : base(repository)
    {
        _identityRepository = identityRepository;
        _addressService = addressService;
        _logicCommonRepository = logicCommonRepository;
    }

    public async Task<URSUserRegisterResponse> Register(URSUserRegisterRequest request)
    {
        var response = new URSUserRegisterResponse() { Success = false };
        
        // Decrypt Key
        var keyDecrypt = _logicCommonRepository.DecryptText(request.Key);
        string[] values = keyDecrypt.Split(",");
        
        string userName = values[0];
        string email = values[1];
        string firstName = values[2];
        string lastName = values[3];
        
        // Begin transaction
        await Repository.ExecuteInTransactionAsync(async () =>
        {
            // Check if the user already exists
            var userSelect = _identityRepository.GetVwUserAuthentication(userName, email);
        
            // If the user already exists, return an error
            if (userSelect != null)
            {
                response.SetMessage(MessageId.E11004);
                return false;
            }
        
            var newUser = new User()
            {
                UserName = userName,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                BirthDate = DateOnly.Parse(request.BirthDay),
                ImageUrl = request.ImageUrl,
            };
            await Repository.AddAsync(newUser);
        
            // Add address
            var newAddress = new Address()
            {
                Username = newUser.UserName,
                AddressLine = request.AddressLine,
                Ward = request.Ward,
                City = request.City,
                Province = request.Province,
                District = request.District,
            };
            await _addressService .AddAsync(newAddress);
        
            // Save changes
            await Repository.SaveChangesAsync(userName);
        
            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        return response;
    }

    public UDSSelectUserProfileResponse SelectUserProfile(IIdentityService identityService)
    {
        var response = new UDSSelectUserProfileResponse { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Get user information
        var userSelect = Repository.FindView(x => x.UserName == userName)
            .Select(x => new UDSSelectUserProfileEntity
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                ImageUrl = x.ImageUrl,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
            })
            .FirstOrDefault();

        // True
        response.Success = true;
        response.Response = userSelect;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    public UDSUpdateUserProfileReponse UpdateUserProfile(UDSUpdateUserProfileRequest request, IIdentityService identityService)
    {
        var response = new UDSUpdateUserProfileReponse() { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
        
        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get user
            var user = Repository.Find(x => x.UserName == userName).FirstOrDefault();

            // Update user information
            if (request.LastName != null)
            {
                user.LastName = request.LastName;
            }
            
            if (request.FirstName != null)
            {
                user.FirstName = request.FirstName;
            }

            if (request.PhoneNumber != null)
            {
                user.PhoneNumber = request.PhoneNumber;
            }

            if (request.ImageUrl != null)
            {
                user.ImageUrl = request.ImageUrl;
            }

            if (request.BirthDay != null)
            {
                user.BirthDate = DateOnly.Parse(request.BirthDay);
            }

            if (request.Gender != null)
            {
                user.Gender = request.Gender;
            }

            // Update
            Repository.Update(user);
            Repository.SaveChanges(user.UserName);

            // True
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return true;
        });
        return response;
    }
}