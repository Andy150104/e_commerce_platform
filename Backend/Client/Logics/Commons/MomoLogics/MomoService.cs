using System.Security.Cryptography;
using System.Text;
using Client.Controllers.V1.ThirdParty;
using Client.Utils.Consts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Server.Utils.Consts;

namespace Client.Logics.Commons.MomoLogics
{
    public class MomoService : IMomoService
    {
        private readonly IOptions<MomoOptionModel> _options;

        public MomoService(IOptions<MomoOptionModel> options)
        {
            _options = options;
        }

        public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(MomoExecuteResponseModel model)
        {
            model.OrderId = DateTime.UtcNow.Ticks.ToString();
            model.OrderInfo = model.FullName + "_" + model.OrderInfo;
            var rawData =
                $"partnerCode={_options.Value.PartnerCode}" +
                $"&accessKey={_options.Value.AccessKey}" +
                $"&requestId={model.OrderId}" +
                $"&amount={model.Amount}" +
                $"&orderId={model.OrderId}" +
                $"&orderInfo={model.OrderInfo}" +
                $"&returnUrl={_options.Value.ReturnUrl}" +
                $"&notifyUrl={_options.Value.NotifyUrl}" +
                $"&extraData=";
            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);
            var client = new RestClient(_options.Value.MomoApiUrl);
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");

            // Create an object representing the request data
            var requestData = new
            {
                accessKey = _options.Value.AccessKey,
                partnerCode = _options.Value.PartnerCode,
                requestType = _options.Value.RequestType,
                notifyUrl = _options.Value.NotifyUrl,
                returnUrl = _options.Value.ReturnUrl,
                orderId = model.OrderId,
                amount = model.Amount.ToString(),
                orderInfo = model.OrderInfo,
                requestId = model.OrderId,
                extraData = "",
                signature = signature
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData),
                ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            var momoResponse = JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content);
            return momoResponse;
        }

        public async Task<MomoCreatePaymentResponseModel> CreatePaymentOrderAsync(MomoExecuteResponseModel model, byte platForm, Guid addressId)
        {
            // Add extra data to the request
            var extraData = $"{model.FullName}|{addressId}|{platForm}"; 
            
            // Create the raw data for the request
            var rawData =
                $"partnerCode={_options.Value.PartnerCode}" +
                $"&accessKey={_options.Value.AccessKey}" +
                $"&requestId={model.OrderId}" +
                $"&amount={model.Amount}" +
                $"&orderId={model.OrderId}" +
                $"&orderInfo={model.OrderInfo}" +
                $"&returnUrl={_options.Value.ReturnUrlOrder}" + 
                $"&notifyUrl={_options.Value.NotifyUrl}" +
                $"&extraData={extraData}";  
            
            // Compute the signature
            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);
            
            // Create the request
            var client = new RestClient(_options.Value.MomoApiUrl);
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");
            
            // Create an object representing the request data to Momo
            var requestData = new
            {
                accessKey = _options.Value.AccessKey,
                partnerCode = _options.Value.PartnerCode,
                notifyUrl = _options.Value.NotifyUrl,
                returnUrl = _options.Value.ReturnUrlOrder,
                requestType = _options.Value.RequestType,
                orderId = model.OrderId,
                amount = model.Amount.ToString(),
                orderInfo = model.OrderInfo,
                requestId = model.OrderId,
                extraData = extraData,
                signature = signature
            };
            
            // Add the request data to the request
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData),
                ParameterType.RequestBody);

            // Execute the request
            var response = await client.ExecuteAsync(request);
            var momoResponse = JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content);
            Console.WriteLine("Raw Momo Response: " + response.Content);

            return momoResponse;
        }

        public MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection)
        {
            var errorCode = collection.First(s => s.Key == "errorCode").Value;
            var amount = collection.First(s => s.Key == "amount").Value;
            var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;
            var transId = collection.First(s => s.Key == "transId").Value;
            var res = orderInfo.ToString().Split('_');

            return new MomoExecuteResponseModel()
            {
                ErrorCode = errorCode,
                FullName = res[1],
                Amount = amount,
                OrderId = res[0],
                OrderInfo = res[2],
                TransactionId = transId,
            };
        }
        
        public MomoExecuteResponseModel PaymentExecuteOrderAsync(IQueryCollection collection)
        {
            var errorCode = collection.First(s => s.Key == "errorCode").Value;
            var amount = collection.First(s => s.Key == "amount").Value;
            var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;
            var transId = collection.First(s => s.Key == "transId").Value;
            var extraData = collection.First(s => s.Key == "extraData").Value;
            var res = extraData.ToString().Split('|');

            return new MomoExecuteResponseModel()
            {
                ErrorCode = errorCode,
                FullName = res[0],
                Amount = amount,
                OrderId = orderId,
                OrderInfo = orderInfo,
                TransactionId = transId,
                ExtraData = extraData,
            };
        }

        public string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }

        public async Task<MomoRefundResponse> CreateRefundAsync(MomoRefundRequest model)
        {
            model.OrderId = DateTime.UtcNow.Ticks.ToString();
            var rawData =
                $"accessKey={_options.Value.AccessKey}" +
                $"&amount={model.Amount}" +
                $"&description={model.Description}" +
                $"&orderId={model.OrderId}" +
                $"&partnerCode={model.PartnerCode}" +
                $"&requestId={model.RequestId}" +
                $"&transId={model.TransId}";
            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);
            var client = new RestClient("https://test-payment.momo.vn/v2/gateway/api/refund");
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");
            var requestData = new
            {
                partnerCode = _options.Value.PartnerCode,
                orderId = model.OrderId,
                requestId = model.RequestId,
                amount = model.Amount,
                transId = model.TransId,
                lang = model.Lang,
                description = model.Description,
                signature = signature
            };
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData),
                ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            var momoResponse = JsonConvert.DeserializeObject<MomoRefundResponse>(response.Content);
            return momoResponse;
        }
    }
}