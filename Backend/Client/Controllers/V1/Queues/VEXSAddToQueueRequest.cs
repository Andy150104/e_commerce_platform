using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.VEXS
{
    public class VEXSAddToQueueRequest : AbstractApiRequest
    {
        [Required(ErrorMessage = "BlindBoxId is required")]
        public Guid BlindBoxId { get; set; }
        
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
