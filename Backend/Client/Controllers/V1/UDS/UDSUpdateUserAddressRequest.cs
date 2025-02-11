using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.UPS;

public class UDSUpdateUserAddressRequest : AbstractApiRequest
{
    
    public Guid AddressId { get; set; }
    
    [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$", ErrorMessage = "First name contains invalid characters")]
    public string? AddressLine { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$", ErrorMessage = "Ward contains invalid characters")]
    public string? Ward { get; set; }

    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "City contains invalid characters")]
    public string? City { get; set; }

    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Country contains invalid characters")]
    public string? Country { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$", ErrorMessage = "District contains invalid characters")]
    public string? District { get; set; }
}