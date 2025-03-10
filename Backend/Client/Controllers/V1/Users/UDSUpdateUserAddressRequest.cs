using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.UDS;

public class UDSUpdateUserAddressRequest : AbstractApiRequest
{
    public Guid AddressId { get; set; }
    
    [RegularExpression(@"^[\p{L}0-9\s\-,./]{1,40}$", ErrorMessage = "First name contains invalid characters")]
    public string? AddressLine { get; set; }

    [RegularExpression(@"^[\p{L}0-9\s\-,./]{1,40}$", ErrorMessage = "Ward contains invalid characters")]
    public string? Ward { get; set; }

    [RegularExpression(@"^[\p{L}0-9\s\-,./]{1,40}$", ErrorMessage = "City contains invalid characters")]
    public string? City { get; set; }

    [RegularExpression(@"^[\p{L}0-9\s\-,./]{1,40}$", ErrorMessage = "Province contains invalid characters")]
    public string? Province { get; set; }

    [RegularExpression(@"^[\p{L}0-9\s\-,./]{1,40}$", ErrorMessage = "District contains invalid characters")]
    public string? District { get; set; }
}