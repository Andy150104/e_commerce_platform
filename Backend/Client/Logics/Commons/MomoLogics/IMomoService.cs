using Client.Controllers.V1.ThirdParty;

namespace Client.Logics.Commons.MomoLogics
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(MomoExecuteResponseModel model);
        
        Task<MomoCreatePaymentResponseModel> CreatePaymentOrderAsync(MomoExecuteResponseModel model, byte platForm, Guid addressId);

        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);

        MomoExecuteResponseModel PaymentExecuteOrderAsync(IQueryCollection collection);
        
        Task<MomoRefundResponse> CreateRefundAsync(MomoRefundRequest model);

        string ComputeHmacSha256(string message, string secretKey);
    }
}
