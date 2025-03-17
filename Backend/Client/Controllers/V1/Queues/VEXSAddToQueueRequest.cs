using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.VEXS
{
    public class VEXSAddToQueueRequest : AbstractApiRequest
    {
        [Required(ErrorMessage = "ExchangeId is required")]
        public Guid ExchangeId { get; set; }
        
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
