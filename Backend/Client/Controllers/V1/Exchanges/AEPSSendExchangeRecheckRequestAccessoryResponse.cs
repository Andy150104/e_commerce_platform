using Client.Models;

namespace Client.Controllers.V1.AEPS;

public class AEPSSendExchangeRecheckRequestAccessoryResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}

