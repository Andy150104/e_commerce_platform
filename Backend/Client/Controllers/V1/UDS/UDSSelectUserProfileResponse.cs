namespace Client.Controllers.V1.UPS;

public class UDSSelectUserProfileResponse : AbstractApiResponse<UDSSelectUserProfileEntity>
{
    public override UDSSelectUserProfileEntity Response { get; set; }
}

public class UDSSelectUserProfileEntity
{
    public string? Email { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ImageUrl { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string Gender { get; set; }
}