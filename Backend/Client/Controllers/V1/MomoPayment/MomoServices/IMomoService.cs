namespace Client.Controllers.V1.MomoPayment.MomoServices
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(MomoExecuteResponseModel model);
        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
        Task<MomoRefundResponse> CreateRefundAsync(MomoRefundRequest model);
    }
}
