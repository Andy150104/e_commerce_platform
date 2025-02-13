using Client.Controllers;
using Client.Models.Helper;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using server.Models;

namespace server.Controllers.V1.DetailOfProductScreenExchange;
/// <summary>
/// HSShowPlanController - Show Plans Home Screen
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DOPSEShowProductByIdController : AbstractApiControllerNotToken<DOPSEShowProductByIdRequest, DOPSEShowProductByIdResponse, DOPSEShowProductByIdEntity>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    public DOPSEShowProductByIdController(AppDbContext context)
    {
        _context = context;
        _context._Logger = logger;
    }
    /// <summary>
    /// Coming Posh
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns
    public override DOPSEShowProductByIdResponse Post(DOPSEShowProductByIdRequest request)
    {
        return Post(request, _context, logger, new DOPSEShowProductByIdResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override DOPSEShowProductByIdResponse Exec(DOPSEShowProductByIdRequest request, IDbContextTransaction transaction)
    {
        var response = new DOPSEShowProductByIdResponse() { Success = false };
        var product = _context.Products.AsNoTracking().SingleOrDefault(p => p.ProductId == request.ProductId);
        if (product == null)
        {
            response.SetMessage(MessageId.E11004);
            return response;    
        }

        response.Response = new DOPSEShowProductByIdEntity
        {
            Description = product.Description,
            ProductId = request.ProductId,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            Username = product.Username
        };

        response.Success = true;
        response.SetMessage(MessageId.I00003);
        return response;
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override DOPSEShowProductByIdResponse ErrorCheck(DOPSEShowProductByIdRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new DOPSEShowProductByIdResponse() { Success = false };
        if (detailErrorList.Count > 0)
        {
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }
        //true
        response.Success = true;
        return response;
    }
}

