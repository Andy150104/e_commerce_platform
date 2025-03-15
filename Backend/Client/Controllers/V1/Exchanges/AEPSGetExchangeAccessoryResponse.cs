
using Client.Models;

namespace Client.Controllers.V1.AEPS;

public class AEPSGetExchangeAccessoryResponse : AbstractApiResponse<List<AEPSGetExchangeAccessoryEntity>>
{
    public override List<AEPSGetExchangeAccessoryEntity> Response { get; set; }
}

public class AEPSGetExchangeAccessoryEntity
{
    public Guid ExchangeId { get; set; }

    public Guid BlindBoxId { get; set; }

    public byte? Status { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public AEPSGetExchangeAccessoryBlindBoxEntity BlindBox { get; set; } = null!;
}

public class AEPSGetExchangeAccessoryBlindBoxEntity
{
    public Guid BlindBoxId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Username { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public Guid? WishlistId { get; set; }

    public virtual ICollection<AEPSGetExchangeAccessoryImagesBlindBoxesEntity> ImagesBlindBoxes { get; set; } = new List<AEPSGetExchangeAccessoryImagesBlindBoxesEntity>();   
}

public class AEPSGetExchangeAccessoryImagesBlindBoxesEntity
{
    public Guid ImageId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}
