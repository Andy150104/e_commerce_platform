using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.ViewQueueExchangeScreen
{
    public class VEXSAddToQueueRequest : AbstractApiRequest
    {
        [Required(ErrorMessage = "BlindBoxId is required")]
        public Guid BlindBoxId { get; set; }
        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
    }
}
