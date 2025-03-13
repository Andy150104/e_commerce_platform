using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.Users;

public class UDSDeleteUserAddressRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "AddressId is required")]
    public Guid AddressId { get; set; }
}