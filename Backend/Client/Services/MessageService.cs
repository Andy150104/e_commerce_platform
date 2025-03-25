using Azure.Messaging;
using Client.Controllers.V1.AEPS;
using Client.Controllers.V1.Message;
using Client.Logics.Commons;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;
using Microsoft.Identity.Client;
using Polly;

namespace Client.Services
{
    public class MessageService : BaseService<Message, Guid, object>, IMessageService
    {
        private readonly IUserService _userService;

        public MessageService(IBaseRepository<Message, Guid, object> repository, IUserService userService) : base(repository)
        {
            _userService = userService;
        }

        /// <summary>
        /// Add Message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public MessageResponse AddMessage(MessageEntityRequest message)
        {
            MessageResponse response = new MessageResponse() { IsSuccess = false };
            var sender = _userService.FindView(x => x.UserName == message.SenderId).FirstOrDefault();
            if (sender == null)
            {
                response.Response = "Sender not exist";
                return response;
            }
            var receiver = _userService.FindView(x => x.UserName == message.ReceiverId).FirstOrDefault();
            if (sender == null)
            {
                response.Response = "Receiver not exist";
                return response;
            }
            Repository.Add(new Message
            {
                Content = message.Content,
                CreatedBy = message.SenderId,
                MessageId = message.MessageId,
                ReceiverId = message.ReceiverId,
                SenderId = message.SenderId,
                Status = message.Status
            });
            Repository.SaveChanges(sender.UserName);

            response.IsSuccess = true;
            response.Response = "Successfully";
            return response;
        }


        public GetMessageResponse GetMessage(GetMessageRequest request, IIdentityService identityService)
        {
            var response = new GetMessageResponse() { Success = false };

            // Get userName
            var userName = identityService.IdentityEntity.UserName;

            Repository.ExecuteInTransaction(() =>
            {
                var messages = Repository.Find(
                    x => x.SenderId == userName || x.ReceiverId == userName,
                    isTracking: false
                ).ToList();

                var conversationGroups = messages.GroupBy(m =>
                {
                    return (m.SenderId == userName) ? m.ReceiverId : m.SenderId;
                });

                var result = new List<GetMessageEntity>();

                foreach (var group in conversationGroups)
                {
                    var otherUserId = group.Key;

                    var lastMsg = group.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
                    if (lastMsg == null) continue;

                    var timeString = lastMsg.CreatedAt;

                    var lastMessageContent = lastMsg.Content ?? "";

                    var userEntity = _userService.GetById(otherUserId);
                    // Tuỳ code, userEntity có FullName, AvatarUrl, ...
                    var userName = userEntity?.UserName ?? otherUserId;
                    var avatar = userEntity?.ImageUrl;

                    // 6. Tạo DTO
                    var dto = new GetMessageEntity
                    {
                        receiverId = otherUserId,
                        receiverName = userEntity.FirstName + userEntity.LastName,
                        avatar = avatar,
                        lastMessage = lastMessageContent,
                        Time = timeString.Value
                    };

                    result.Add(dto);

                    response.Response = result;
                    response.Success = true;
                    response.SetMessage(MessageId.I00001);
                }
                return true;
            });
            return response;
        }

        /// <summary>
        /// Get message with entity
        /// </summary>
        /// <param name="request"></param>
        /// <param name="identityService"></param>
        /// <returns></returns>
        public GetMessageWUserNameResponse GetMessageWEntity(GetMessageWUserNameRequest request, IIdentityService identityService)
        {
            var response = new GetMessageWUserNameResponse() { Success = false };

            // Get userName
            var userName = identityService.IdentityEntity.UserName;

            Repository.ExecuteInTransaction(() =>
            {
                var message = Repository.Find(x => (x.SenderId == userName && x.ReceiverId == request.UserName) || (x.SenderId == request.UserName && x.ReceiverId == userName), isTracking: false).OrderBy(x => x.CreatedAt).Select(x => new GetMessageWEntity
                {
                    conversationId = x.MessageId.ToString(),
                    sender = (x.SenderId == userName) ? "mine" : "other",
                    content = x.Content,
                    createdAt = x.CreatedAt
                }).ToList();

                response.Response = message;
                response.SetMessage(MessageId.I00001);
                response.Success = true;
                return true;
            });
            return response;
        }
    }

    public class MessageEntityRequest
    {
        public Guid MessageId { get; set; }

        public string? Content { get; set; }

        public string? Status { get; set; }

        public string SenderId { get; set; } = null!;

        public string ReceiverId { get; set; } = null!;
    }

    public class MessageResponse
    {
        public bool IsSuccess { get; set; }
        public string Response { get; set; }
    }

}
