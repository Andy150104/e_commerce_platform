using System.ComponentModel.DataAnnotations;
using Client.Controllers;
using Client.Utils.Consts;

namespace server.Controllers.V1.UserRegisterScreen;

public class URSUserRegisterRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "UserName not null")]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "Username must not contain special characters and must not start with a number")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email not null")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format, please enter the correct email format")]
    public string Email { get; set; }
    
    [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format")]
    public string PhoneNumber { get; set; }
    
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid date format, please use YYYY-MM-DD")]
    public string BirthDay { get; set; }

    
    [StringLength(100, ErrorMessage = "Full name cannot be longer than 100 characters")]
    public string FullName { get; set; }
    
    [Url(ErrorMessage = "Invalid URL format")]
    public string ImageUrl { get; set; }
    
    [Range(1,3, ErrorMessage = "Gender must be 1, 2 or 3")]
    public byte Gender { get; set; }

    [Required(ErrorMessage = "PlanId not null")]
    public Guid PlanId { get; set; }
}