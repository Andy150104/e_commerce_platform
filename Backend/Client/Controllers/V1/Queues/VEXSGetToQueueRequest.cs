using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.AEPS;

public class VEXSGetToQueueRequest : AbstractApiRequest
{
    [Required]
    public Guid ExchangeId { get; set; }
    public string? SearchName { get; set; } = null;
}