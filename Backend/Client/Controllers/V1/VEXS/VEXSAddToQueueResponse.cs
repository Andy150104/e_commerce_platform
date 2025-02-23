using Client.Controllers;
using server.Models;

namespace Client.Controllers.V1.ViewQueueExchangeScreen
{
    public class VEXSAddToQueueResponse : AbstractApiResponse<string>
    {
        public override string Response { get; set; }
    }
}
