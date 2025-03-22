using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.VEXS
{
    public class VEXSApproveQueueRequest : AbstractApiRequest
    {
        public Guid ExchangeId { get; set; }
        
        public Guid QueueId { get; set; }
    }
}
