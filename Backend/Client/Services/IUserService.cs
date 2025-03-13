using Client.Controllers.V1.UDS;
using Client.Controllers.V1.UserRegisterScreen;
using Client.Models;

namespace Client.Services;

public interface IUserService : IBaseService<User, string, VwUserProfile>
{
    public Task<URSUserRegisterResponse> Register(URSUserRegisterRequest request);
    
    UDSSelectUserProfileResponse SelectUserProfile(IIdentityService identityService);
    
    UDSUpdateUserProfileReponse UpdateUserProfile(UDSUpdateUserProfileRequest request, IIdentityService identityService);
}