using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.AEPS;

public class AEPSCheckExchangeRecheckRequestAccessoryRequest : AbstractApiRequest
{
    public Guid RequestId { get; set; }
    public bool isAccepted { get; set; }
}