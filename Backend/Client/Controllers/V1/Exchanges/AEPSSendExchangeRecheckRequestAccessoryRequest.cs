using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.AEPS;

public class AEPSSendExchangeRecheckRequestAccessoryRequest : AbstractApiRequest
{
    public Guid ExchangeId { get; set; }
    public string? Description { get; set; } 
}