using Client.Controllers;
using Client.Models.Helper;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using server.Controllers.V1.HomeScreen;
using server.Logics.Commons;
using System.ComponentModel.DataAnnotations;

namespace server.Controllers.V1.ForgetPasswordScreen;

[Route("api/v1/[controller]")]
[ApiController]
public class FPSUpdatePasswordController : AbstractApiControllerNotToken<FPSUpdatePasswordRequest, FPSUpdatePasswordResponse, string>
{
    private readonly AppDbContext _context;
    private readonly IMemoryCache _cache;
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();


    public FPSUpdatePasswordController(AppDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
        _context._Logger = logger;

    }
    public override FPSUpdatePasswordResponse Post(FPSUpdatePasswordRequest request)
    {
        return Post(request, _context, logger, new FPSUpdatePasswordResponse());
    }

    protected override FPSUpdatePasswordResponse Exec(FPSUpdatePasswordRequest request, IDbContextTransaction transaction)
    {
        var response = new FPSUpdatePasswordResponse() { Success = false};

        var user = _context.Users.AsNoTracking().FirstOrDefault(u => u.Email == request.Email || u.UserName == request.UserName);
        if (user == null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }

        var mailTemple = _context.EmailTemplates.AsNoTracking().FirstOrDefault(et => et.IsActive == true && et.ScreenName == "OTP");
        if(mailTemple == null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }

        var otp = new Random().Next(100000, 999999).ToString();
        if(request.Email != null) 
        _cache.Set(request.Email, otp, TimeSpan.FromMinutes(5));
        if(request.UserName != null)
        _cache.Set(request.UserName, otp, TimeSpan.FromMinutes(5));

        SendMailLogic.SendMail(mailTemple, user.Email!, _context, response.DetailErrorList);

        response.Success = true;
        response.SetMessage(MessageId.E00000, "OTP Sent successfully");
        return response;
    }

    protected internal override FPSUpdatePasswordResponse ErrorCheck(FPSUpdatePasswordRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new FPSUpdatePasswordResponse() { Success = false };

        if (detailErrorList.Count > 0)
        {
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }

        response.Success = true;
        return response;
    }
}


