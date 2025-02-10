using Client.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace server.Controllers.V1.ForgetPasswordScreen
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FPSVerifyTokenController : AbstractApiControllerNotToken<FPSUpdatePasswordRequest, FPSUpdatePasswordResponse, string>
    {



        public override FPSUpdatePasswordResponse Post(FPSUpdatePasswordRequest request)
        {
            throw new NotImplementedException();
        }

        protected override FPSUpdatePasswordResponse Exec(FPSUpdatePasswordRequest request, IDbContextTransaction transaction)
        {
            throw new NotImplementedException();
        }

        protected internal override FPSUpdatePasswordResponse ErrorCheck(FPSUpdatePasswordRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
