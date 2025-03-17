
using Client.Models;

namespace Client.Controllers.V1.AEPS;

public class AEPSGetFailExchangeAccessoryResponse : AbstractApiResponse<List<AEPSGetFailExchangeAccessoryEntity>>
{
    public override List<AEPSGetFailExchangeAccessoryEntity> Response { get; set; }
}

public class AEPSGetFailExchangeAccessoryEntity
{
    public Guid ExchangeId { get; set; }

    public string ExchangeName { get; set; }

    public string Description { get; set; }

    public byte? Status { get; set; }

    public Guid BlindBoxId { get; set; }

    public ICollection<AEPSGetFailExchangeAccessoryImageBlindBoxList> imageBlindBoxList = new List<AEPSGetFailExchangeAccessoryImageBlindBoxList>();
}

public class AEPSGetFailExchangeAccessoryImageBlindBoxList
{
    public Guid ImageId { get; set; }

    public string ImageUrls { get; set; } = null!;
}

