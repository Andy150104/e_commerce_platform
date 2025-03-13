
using Client.Models;

namespace Client.Controllers.V1.AEPS;

public class AEPSGetExchangeRecheckRequestAccessoryResponse : AbstractApiResponse<List<AEPSGetExchangeRecheckRequestAccessoryEntity>>
{
    public override List<AEPSGetExchangeRecheckRequestAccessoryEntity> Response { get; set; }
}

public class AEPSGetExchangeRecheckRequestAccessoryEntity
{
    public Guid RequestId { get; set; }

    public Guid ExchangeId { get; set; }

    public string Description { get; set; }

    public byte Status { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}