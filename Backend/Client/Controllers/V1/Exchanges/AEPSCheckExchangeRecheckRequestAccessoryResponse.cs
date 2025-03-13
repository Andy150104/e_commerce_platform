using Client.Models;

namespace Client.Controllers.V1.AEPS;

public class AEPSCheckExchangeRecheckRequestAccessoryResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}

