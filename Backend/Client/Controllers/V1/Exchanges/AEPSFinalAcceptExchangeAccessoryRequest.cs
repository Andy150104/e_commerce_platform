using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.AEPS;

public class AEPSFinalAcceptExchangeAccessoryRequest : AbstractApiRequest
{
    public Guid ExchangeId { get; set; }
    public Guid QueueId { get; set; }
    public bool isAccepted { get; set; }
}