using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.AEPS;

public class AEPSGetByIdExchangeAccessoryRequest : AbstractApiRequest
{
    public Guid ExchangeId { get; set; }
}