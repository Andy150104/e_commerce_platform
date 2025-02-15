using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.UPS;

public class UDSUpdateUserProfileRequest : AbstractApiRequest
{

    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "First name contains invalid characters")]
    public string FirstName { get; set; }
    
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "First name contains invalid characters")]
    public string LastName { get; set; }
    
    [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format")]
    public string? PhoneNumber { get; set; }
    
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid date format, please use YYYY-MM-DD")]
    public string? BirthDay { get; set; }
    
    [Url(ErrorMessage = "Invalid URL format")]
    public string? ImageUrl { get; set; }
    
    [Range(1,3, ErrorMessage = "Gender must be 1, 2 or 3")]
    public byte? Gender { get; set; }
}