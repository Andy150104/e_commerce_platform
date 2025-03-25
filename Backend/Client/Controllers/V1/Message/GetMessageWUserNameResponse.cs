
using Client.Models;

namespace Client.Controllers.V1.Message;

public class GetMessageWUserNameResponse : AbstractApiResponse<List<GetMessageWEntity>>
{
    public override List<GetMessageWEntity> Response { get; set; }
}

public class GetMessageWEntity
{
    public string conversationId { get; set; }
    public string sender {  get; set; }
    public string content { get; set; }
    public DateTime? createdAt { get; set; }
}

