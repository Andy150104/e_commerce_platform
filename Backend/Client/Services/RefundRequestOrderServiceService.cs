using Client.Controllers.V1.RefundRequestOrders;
using Client.Models;
using Client.Repositories;
using Client.Utils.Consts;

namespace Client.Services;

/// <summary>
/// RefundRequestOrderServiceService - Service for RefundRequestOrder
/// </summary>
public class RefundRequestOrderServiceService : BaseService<RefundRequestsOrder, Guid, VwRefundRequestOrder>, IRefundRequestOrderService
{
    private readonly IOrderService _orderService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="orderService"></param>
    public RefundRequestOrderServiceService(IBaseRepository<RefundRequestsOrder, Guid, VwRefundRequestOrder> repository,
        IOrderService orderService) : base(repository)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Insert refund request order
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public RROInsertRefundRequestOrderResponse InsertRefundRequestOrder(RROInsertRefundRequestOrderRequest request,
        IIdentityService identityService)
    {
        var response = new RROInsertRefundRequestOrderResponse { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Check if order exists
            var order = _orderService.Find(x => x.OrderId == request.OrderId
                                                && x.Username == userName
                                                && x.IsActive == true).FirstOrDefault();
            if (order == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.OrderNotFound);
                return;
            }

            // Insert refund request
            var refundRequestOrder = new RefundRequestsOrder
            {
                OrderId = request.OrderId,
                UserName = userName,
                RefundAmount = order.TotalPrice,
                RefundReason = request.RefundReason,
                ImageUrl = request.ImageUrl,
                RefundStatus = (byte)ConstantEnum.RefundOrderStatus.PendingApproval,
                PaymentMethod = (byte)ConstantEnum.PaymentMethod.Online,
            };

            Add(refundRequestOrder);
            SaveChanges(userName);

            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }

    public RROUpdateRefundRequestOrderResponse UpdateRefundRequestOrder(RROUpdateRefundRequestOrderRequest request, IIdentityService identityService)
    {
        var response = new RROUpdateRefundRequestOrderResponse { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
        
        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get refund request order
            var refundRequestOrder = Find(x => x.RefundId == request.RefundId && x.IsActive == true).FirstOrDefault();
            if (refundRequestOrder == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.RefundRequestOrderNotFound);
                return;
            }
            
            // Update
            if (request.RefundReason != null)
            {
                refundRequestOrder.RefundReason = request.RefundReason;
            }
            if (request.ImageUrl != null)
            {
                refundRequestOrder.ImageUrl = request.ImageUrl;
            }
            Update(refundRequestOrder);
            SaveChanges(userName);
            
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }

    /// <summary>
    /// Delete refund request order
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public RRODeleteRefundRequestOrderResponse DeleteRefundRequestOrder(RRODeleteRefundRequestOrderRequest request,
        IIdentityService identityService)
    {
        var response = new RRODeleteRefundRequestOrderResponse { Success = false };

        // Get userName
        var userName = identityService.IdentityEntity.UserName;

        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get refund request order
            var refundRequestOrder = Find(x => x.RefundId == request.RefundId && x.IsActive == true).FirstOrDefault();
            if (refundRequestOrder == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.RefundRequestOrderNotFound);
                return;
            }

            if (refundRequestOrder.RefundStatus == (byte)ConstantEnum.RefundOrderStatus.Approved 
                || refundRequestOrder.RefundStatus == (byte)ConstantEnum.RefundOrderStatus.Rejected)
            {
                response.SetMessage(MessageId.I00000, "Cannot delete refund request order that has been Approved/ Rejected.");
                return;
            }

            // Delete
            Update(refundRequestOrder);
            SaveChanges(userName, true);

            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });

        return response;
    }

    /// <summary>
    /// Select refund request orders
    /// </summary>
    /// <returns></returns>
    public RROSelectRefundRequestOrdersResponse SelectRefundRequestOrders()
    {
        var response = new RROSelectRefundRequestOrdersResponse { Success = false };

        // Get refund request orders
        var refundRequestOrders = FindView()
            .Select(x => new RROSelectRefundRequestOrdersEntity
            {
                RefundId = x!.RefundId,
                OrderId = x.OrderId,
                UserName = x.UserName,
                PaymentMethod = x.PaymentMethod,
                RefundAmount = x.RefundAmount,
                RefundStatus = x.RefundStatus,
                CreatedAt = x.CreatedAt,
            })
            .ToList();

        response.Success = true;
       // response.Response = refundRequestOrders;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Select refund request order
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public RROSelectRefundRequestOrderResponse SelectRefundRequestOrder(RROSelectRefundRequestOrderRequest request)
    {
        var response = new RROSelectRefundRequestOrderResponse { Success = false };
        
        // Get refund request order
        var refundRequestOrder = FindView(x => x.RefundId == request.RefundId)
            .Select(x => new RROSelectRefundRequestOrderEntity
            {
                RefundId = x!.RefundId,
                OrderId = x.OrderId,
                UserName = x.UserName,
                PaymentMethod = x.PaymentMethod,
                RefundAmount = x.RefundAmount,
                RefundStatus = x.RefundStatus,
                CreatedAt = x.CreatedAt,
                RefundReason = x.RefundReason,
                ApprovedAt = x.ApprovedAt,
                ProcessedBy = x.ProcessedBy,
                RejectedReason = x.RejectedReason,
                UpdatedAt = x.UpdatedAt,
            })
            .FirstOrDefault();
        
        response.Success = true;
        //response.Response = refundRequestOrder;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Approve refund request order
    /// </summary>
    /// <param name="request"></param>
    /// <param name="identityService"></param>
    /// <returns></returns>
    public RROApproveRefundRequestOrderResponse ApproveRefundRequestOrder(RROApproveRefundRequestOrderRequest request, IIdentityService identityService)
    {
        var response = new RROApproveRefundRequestOrderResponse { Success = false };
        
        // Get userName
        var userName = identityService.IdentityEntity.UserName;
        
        // Begin transaction
        Repository.ExecuteInTransaction(() =>
        {
            // Get refund request order
            var refundRequestOrder = Find(x => x.RefundId == request.RefundId && x.IsActive == true).FirstOrDefault();
            if (refundRequestOrder == null)
            {
                response.SetMessage(MessageId.I00000, CommonMessages.RefundRequestOrderNotFound);
                return;
            }
            
            // Update
            if (request.IsApproved)
            {
                refundRequestOrder.RefundStatus = (byte)ConstantEnum.RefundOrderStatus.Approved;
            }
            else
            {
                if (request.RejectedReason == null)
                {
                    response.SetMessage(MessageId.I00000, "If you Rejected, RejectedReason is required.");
                    return;
                }
                refundRequestOrder.RefundStatus = (byte)ConstantEnum.RefundOrderStatus.Rejected;
                refundRequestOrder.RejectedReason = request.RejectedReason;
            }
            refundRequestOrder.ProcessedBy = userName;
            refundRequestOrder.ApprovedAt = DateTime.Now;
            
            Update(refundRequestOrder);
            SaveChanges(userName);
            
            response.Success = true;
            response.SetMessage(MessageId.I00001);
        });
        return response;
    }
}