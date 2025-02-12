using Client.Controllers;
using Client.Models.Helper;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using server.Models;

namespace server.Controllers.V1.HomeScreen;
format code lại 
    /// <summary>
    /// HSShowPlanController - Show Plans Home Screen
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HSShowPlanController : AbstractApiControllerNotToken<HSShowPlanRequest, HSShowPlanResponse, List<VwPlan>>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly AppDbContext _context;

        public HSShowPlanController(AppDbContext context)
        {
            _context = context;
            _context._Logger = logger;
        }
sửa lại đi Hùng
/// <summary>
/// Incomng Post
/// </summary>
/// <param name="request"></param>
/// <returns></returns>
        public override HSShowPlanResponse Post(HSShowPlanRequest request)
        {
            return Post(request, _context, logger, new HSShowPlanResponse());
        }

       Không cần cmt thêm Main processing là được r
        /// <summary>
        /// Main processing - Retrieved VwPlan table from database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        protected override HSShowPlanResponse Exec(HSShowPlanRequest request, IDbContextTransaction transaction)
        {
            var response = new HSShowPlanResponse() { Success = false };

            var plans = _context.VwPlans.AsNoTracking().ToList();
            
            xài !Any
            if (plans == null || plans.Count == 0)
            {
                response.SetMessage(MessageId.E00000, "No plans found");
                return response;
            }

            response.Response = plans;
            response.Success = true;
            response.SetMessage(MessageId.I00001, "Plan retrieved successfully");
            return response;
        }
        
        viết bình thường thôi đang xài design template nên sửa lại cho giống
    /// <summary>
    /// ERROR CHECK
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
        protected internal override HSShowPlanResponse ErrorCheck(HSShowPlanRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
        {
            var response = new HSShowPlanResponse() { Success=false };

            if (detailErrorList.Count > 0)
            {
                response.SetMessage(MessageId.E10000);
                response.DetailErrorList = detailErrorList;
                return response;
            }
//true
            response.Success=true;
            return response;    
        }
    }

 