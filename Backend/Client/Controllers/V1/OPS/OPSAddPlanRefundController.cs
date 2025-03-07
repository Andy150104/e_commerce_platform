﻿using Client.Controllers.V1.OnlinePaymentScreen;
using Client.Models;
using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.OPS;

/// <summary>
/// OPSAddPlanRefundController - buying plan
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class OPSAddPlanRefundController : AbstractApiController<OPSAddPlanRefundRequest, OPSAddPlanRefundResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityapiclient"></param>
    public OPSAddPlanRefundController(AppDbContext context, IIdentityApiClient identityapiclient)
    {
        _context = context;
        _context._Logger = logger;
        _identityApiClient = identityapiclient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns
    [HttpPost]
    [Authorize(AuthenticationSchemes =
        OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override OPSAddPlanRefundResponse Post(OPSAddPlanRefundRequest request)
    {
        return Post(request, _context, logger, new OPSAddPlanRefundResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override OPSAddPlanRefundResponse Exec(OPSAddPlanRefundRequest request, IDbContextTransaction transaction)
    {
        var response = new OPSAddPlanRefundResponse() { Success = false };

        // Get userName
        var username = _context.IdentityEntity.UserName;

        // Get OrderPlan
        var orderPlan = _context.OrderPlans.FirstOrDefault(op => op.OrderId == request.OrderPlanId);
        if (orderPlan == null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }

        // Get RefundPlanRequest
        var refundRequest = new RefundPlanRequest
        {
            RefundRequests = Guid.NewGuid(),
            Status = (byte)RefundRequestEnum.Pending,
            OrderPlanId = orderPlan.OrderId,
            Reason = request.Reason,
        };

        // Add RefundPlanRequest
        _context.RefundPlanRequests.Add(refundRequest);
        _context.SaveChanges(username);
        transaction.Commit();

        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailerrorlist"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override OPSAddPlanRefundResponse ErrorCheck(OPSAddPlanRefundRequest request, List<DetailError> detailerrorlist, IDbContextTransaction transaction)
    {
        var response = new OPSAddPlanRefundResponse() { Success = false };
        if (detailerrorlist.Count > 0)
        {
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailerrorlist;
            return response;
        }

        // True
        response.Success = true;
        return response;
    }
}