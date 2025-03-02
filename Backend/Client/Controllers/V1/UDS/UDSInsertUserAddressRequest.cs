using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.UDS;

public class UDSInsertUserAddressRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "AddressLine not null")]
    [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$", ErrorMessage = "First name contains invalid characters")]
    public string? AddressLine { get; set; }

    [Required(ErrorMessage = "Ward not null")]
    [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$", ErrorMessage = "Ward contains invalid characters")]
    public string? Ward { get; set; }

    [Required(ErrorMessage = "City not null")]
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "City contains invalid characters")]
    public string? City { get; set; }

    [Required(ErrorMessage = "Province not null")]
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Province contains invalid characters")]
    public string? Province { get; set; }
    
    [Required(ErrorMessage = "District not null")]
    [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$", ErrorMessage = "District contains invalid characters")]
    public string? District { get; set; }
}