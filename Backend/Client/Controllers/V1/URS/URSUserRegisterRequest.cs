using System.ComponentModel.DataAnnotations;
using Client.Controllers;
using Client.Utils.Consts;

namespace server.Controllers.V1.UserRegisterScreen;

public class URSUserRegisterRequest : AbstractApiRequest
{
    
    [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format")]
    public string? PhoneNumber { get; set; }
    
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid date format, please use YYYY-MM-DD")]
    public string? BirthDay { get; set; }
    
    [Url(ErrorMessage = "Invalid URL format")]
    public string? ImageUrl { get; set; }
    
    [Range(1,3, ErrorMessage = "Gender must be 1, 2 or 3")]
    public byte? Gender { get; set; }
    
    [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$", ErrorMessage = "First name contains invalid characters")]
    public string? AddressLine { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$", ErrorMessage = "Ward contains invalid characters")]
    public string? Ward { get; set; }

    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "City contains invalid characters")]
    public string? City { get; set; }

    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Province contains invalid characters")]
    public string? Province { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$", ErrorMessage = "District contains invalid characters")]
    public string? District { get; set; }
    
    [Required(ErrorMessage = "Key not null")]
    public string Key { get; set; }
    public Guid? PlanId { get; set; }
}