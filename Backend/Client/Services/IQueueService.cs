using Client.Controllers.V1.VEXS;
using Client.Models;

namespace Client.Services;

public interface IQueueService : IBaseService<Queue, Guid, object>
{
    VEXSAddToQueueResponse AddToQueue(VEXSAddToQueueRequest request, IIdentityService identityService);
}