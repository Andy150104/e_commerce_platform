
using Client.Models;

namespace Client.Controllers.V1.AEPS;

public class AEPSGetFailExchangeAccessoryResponse : AbstractApiResponse<List<AEPSGetFailExchangeAccessoryEntity>>
{
    public override List<AEPSGetFailExchangeAccessoryEntity> Response { get; set; }
}

public class AEPSGetFailExchangeAccessoryEntity
{
    public Guid ExchangeId { get; set; }

    public Guid BlindBoxId { get; set; }

    public byte? Status { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public AEPSGetFailExchangeAccessoryBlindBoxEntity BlindBox { get; set; } = null!;
}

public class AEPSGetFailExchangeAccessoryBlindBoxEntity
{
    public Guid BlindBoxId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Username { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public Guid? WishlistId { get; set; }

    public virtual ICollection<AEPSGetFailExchangeAccessoryImagesBlindBoxesEntity> ImagesBlindBoxes { get; set; } = new List<AEPSGetFailExchangeAccessoryImagesBlindBoxesEntity>();   
}

public class AEPSGetFailExchangeAccessoryImagesBlindBoxesEntity
{
    public Guid ImageId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}
