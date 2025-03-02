namespace Client.Controllers.V1.UDS;

public class UDSSelectUserProfileResponse : AbstractApiResponse<UDSSelectUserProfileEntity>
{
    public override UDSSelectUserProfileEntity Response { get; set; }
}

public class UDSSelectUserProfileEntity
{
    public string? Email { get; set; }

    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ImageUrl { get; set; }

    public DateOnly? BirthDate { get; set; }

    public byte? Gender { get; set; }
}