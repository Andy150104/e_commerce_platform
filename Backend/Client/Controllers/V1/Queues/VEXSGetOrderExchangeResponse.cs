
using Client.Models;

namespace Client.Controllers.V1.AEPS;

public class VEXSGetOrderExchangeResponse : AbstractApiResponse<List<VEXSGetOrderExchangeResponseEntity>>
{
    public override List<VEXSGetOrderExchangeResponseEntity> Response { get; set; }
}

public class VEXSGetOrderExchangeResponseEntity
{
    public Guid ExchangeId { get; set; }
    
    public string ExchangeName { get; set; }
}
