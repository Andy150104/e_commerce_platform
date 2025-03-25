
using Client.Models;

namespace Client.Controllers.V1.Message;

public class GetMessageResponse : AbstractApiResponse<List<GetMessageEntity>>
{
    public override List<GetMessageEntity> Response { get; set; }
}

public class GetMessageEntity
{
    public string receiverId { get; set; }
    public string receiverName { get; set; }
    public string avatar {  get; set; }
    public DateTime Time {  get; set; }
    public string lastMessage { get; set; }
}

