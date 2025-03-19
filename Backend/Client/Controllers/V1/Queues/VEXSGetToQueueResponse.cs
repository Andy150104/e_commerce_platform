
using Client.Models;

namespace Client.Controllers.V1.AEPS;

public class VEXSGetToQueueResponse : AbstractApiResponse<List<VEXSGetToQueueResponseEntity>>
{
    public override List<VEXSGetToQueueResponseEntity> Response { get; set; }
}

public class VEXSGetToQueueResponseEntity
{
    public Guid QueueId { get; set; }

    public string userFullNameQueue { get; set; }

    public string UserImage { get; set; }

    public string DescriptionQueue { get; set; }

    public string UserGender { get; set; }

    public string UserPhone { get; set; }

    public DateOnly userBirthday { get; set; }

    public byte? Status { get; set; }
}
