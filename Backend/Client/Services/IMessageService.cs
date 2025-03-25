using Client.Controllers.V1.Message;
using Client.Models;

namespace Client.Services
{
    public interface IMessageService : IBaseService<Message, Guid , object>
    {
        public MessageResponse AddMessage(MessageEntityRequest message);
        public GetMessageWUserNameResponse GetMessageWEntity(GetMessageWUserNameRequest request, IIdentityService identityService);
        public GetMessageResponse GetMessage(GetMessageRequest request, IIdentityService identityService);
    }
}
