using System.Text;
using Client.Models;
using Newtonsoft.Json;

namespace Client.Logics.Commons.GHNLogics;

public class GHNLogics : IGHNLogic
{
    private readonly HttpClient _httpClient;
    private readonly string _apiToken = "880e415a-fc97-11ef-82e7-a688a46b55a3"; 
    private readonly string _shopId = "196113";
    private readonly string _createOrderUrl = "https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/create";

    public GHNLogics(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Token", _apiToken);
        _httpClient.DefaultRequestHeaders.Add("ShopId", _shopId);
    }
    
    public async Task<CreateOrderGHNResponse> CreateOrderGHNAsync(List<OrderDetail> orderDetails, User user, Address address, List<KeyValuePair<string, string>> accessoryNames)
    {
        var response = new CreateOrderGHNResponse();
        var request = new CreateOrderGHNRequest
        {
            //$"{user.LastName} {user.FirstName}"
            ToName = "Khoiii",
            ToPhone = "0987654321",
            ToAddress = address.AddressLine,
            ToWardCode = "20308",
            ToDistrictId = 1444,
        };

        request.Items = orderDetails.Select(x => new GhnOrderItem
        {
            Code = x.AccessoryId,
            Name = accessoryNames.FirstOrDefault(a => a.Key == x.AccessoryId).Value,
            Quantity = x.Quantity,
            Price = Convert.ToInt32(x.UnitPrice),
            Length = 10, 
            Width = 10, 
            Height = 10, 
            Weight = 500
        }).ToList();
        
        
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.PostAsync(_createOrderUrl, httpContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                response.Message = "GHN API Error: " + jsonResponse;
                return response;
            }

            var ghnResponse = JsonConvert.DeserializeObject<CreateOrderGHNResponse>(jsonResponse);
            if (ghnResponse != null && ghnResponse.Code == 200)
            {
                response = ghnResponse;
            }
        }
        catch (Exception ex)
        {
            response.Message = "Exception: " + ex.Message;
        }

        return response;
    }
    
}