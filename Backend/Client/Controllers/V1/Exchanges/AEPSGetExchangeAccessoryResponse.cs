
using Client.Models;

namespace Client.Controllers.V1.AEPS;

public class AEPSGetExchangeAccessoryResponse : AbstractApiResponse<List<AEPSGetExchangeAccessoryEntity>>
{
    public override List<AEPSGetExchangeAccessoryEntity> Response { get; set; }
}

public class AEPSGetExchangeAccessoryEntity
{
    public Guid ExchangeId { get; set; }

    public string ExchangeName { get; set; }

    public string Description { get; set; }

    public byte? Status { get; set; }

    public Guid BlindBoxId { get; set; }

    public ICollection<AEPSGetExchangeAccessoryImageBlindBoxList> imageBlindBoxList { get; set; }
}

public class AEPSGetExchangeAccessoryImageBlindBoxList
{
    public Guid ImageId { get; set; }

    public string ImageUrls { get; set; } = null!;
}
