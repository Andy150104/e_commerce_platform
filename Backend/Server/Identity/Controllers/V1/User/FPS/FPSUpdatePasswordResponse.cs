using Client.Controllers;

namespace Server.Controllers.V1.ForgetPasswordScreen;

    public class FPSUpdatePasswordResponse : AbstractApiResponse<string>
    {
        public override string Response { get; set; }
    }

