using Client.Controllers;
using server.Models;

namespace Client.Controllers.V1.DisplayProductScreenExchange
{
    public class DPSESAddToWishlistResponse : AbstractApiResponse<string>
    {
        public override string Response { get; set; }
    }
}
