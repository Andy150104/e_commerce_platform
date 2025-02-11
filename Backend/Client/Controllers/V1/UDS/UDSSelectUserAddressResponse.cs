namespace Client.Controllers.V1.UPS;

public class UDSSelectUserAddressResponse : AbstractApiResponse<List<UDSSelectUserAddressEntity>>
{
    public override List<UDSSelectUserAddressEntity> Response { get; set; }
}

public class UDSSelectUserAddressEntity
{
    public Guid AddressId { get; set; }
    
    public string FullName { get; set; }
    public string? AddressLine { get; set; }

    public string? Ward { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? District { get; set; }
    
}