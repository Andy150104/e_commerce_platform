using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.Message;

public class GetMessageWUserNameRequest : AbstractApiRequest
{
    public string UserName { get; set; }
}