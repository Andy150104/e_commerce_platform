
using Client.Models;

namespace Client.Controllers.V1.AEPS;

public class AEPSGetByIdExchangeAccessoryResponse : AbstractApiResponse<AEPSGetByIdExchangeAccessoryEntity>
{
    public override AEPSGetByIdExchangeAccessoryEntity Response { get; set; }
}

public class AEPSGetByIdExchangeAccessoryEntity
{
    public Guid ExchangeId { get; set; }

    public string ExchangeName { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public byte? Status { get; set; }

    public Guid BlindBoxId { get; set; }

    public ICollection<AEPSGetByIdExchangeAccessoryImageList> imageBlindBoxList { get; set; }
}

public class AEPSGetByIdExchangeAccessoryImageList
{
    public Guid ImageId { get; set; }

    public string ImageUrls { get; set; } = null!;
}
