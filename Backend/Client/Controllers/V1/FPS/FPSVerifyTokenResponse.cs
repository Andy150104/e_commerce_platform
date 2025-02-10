using Client.Controllers;

namespace server.Controllers.V1.ForgetPasswordScreen
{
    public class FPSVerifyTokenResponse : AbstractApiResponse<string>
    {
        public override string Response { get; set; }
    }
}
