namespace Client.Logics.Commons.GHNLogics;

using Newtonsoft.Json;

public class CreateOrderGHNRequest
{
    [JsonProperty("payment_type_id")]
    public int PaymentTypeId { get; set; } = 2;

    [JsonProperty("note")]
    public string Note { get; set; } = "Tintest 123";

    [JsonProperty("required_note")]
    public string RequiredNote { get; set; } = "KHONGCHOXEMHANG";

    // Sender Information
    [JsonProperty("from_name")]
    public string FromName { get; set; } = "TinTest124";

    [JsonProperty("from_phone")]
    public string FromPhone { get; set; } = "0987654321";

    [JsonProperty("from_address")]
    public string FromAddress { get; set; } = "72 Thành Thái, Phường 14, Quận 10, Hồ Chí Minh, Vietnam";

    [JsonProperty("from_ward_name")]
    public string FromWardName { get; set; } = "Phường 14";

    [JsonProperty("from_district_name")]
    public string FromDistrictName { get; set; } = "Quận 10";

    [JsonProperty("from_province_name")]
    public string FromProvinceName { get; set; } = "HCM";

    // Return Information
    [JsonProperty("return_phone")]
    public string ReturnPhone { get; set; } = "0332190444";

    [JsonProperty("return_address")]
    public string ReturnAddress { get; set; } = "39 NTT";

    [JsonProperty("return_district_id")]
    public int? ReturnDistrictId { get; set; } = null;

    [JsonProperty("return_ward_code")]
    public string ReturnWardCode { get; set; } = "";

    [JsonProperty("client_order_code")]
    public string ClientOrderCode { get; set; } = "";

    // Receiver Information
    [JsonProperty("to_name")]
    public string ToName { get; set; } = "TinTest124";

    [JsonProperty("to_phone")]
    public string ToPhone { get; set; } = "0987654321";

    [JsonProperty("to_address")]
    public string ToAddress { get; set; } = "72 Thành Thái, Phường 14, Quận 10, Hồ Chí Minh, Vietnam";

    [JsonProperty("to_ward_code")]
    public string ToWardCode { get; set; } = "20308";

    [JsonProperty("to_district_id")]
    public int ToDistrictId { get; set; } = 1444;

    // Order Details
    [JsonProperty("cod_amount")]
    public int CodAmount { get; set; } = 200000;

    [JsonProperty("content")]
    public string Content { get; set; } = "Theo New York Times";

    // Package Information
    [JsonProperty("weight")]
    public int Weight { get; set; } = 200;

    [JsonProperty("length")]
    public int Length { get; set; } = 1;

    [JsonProperty("width")]
    public int Width { get; set; } = 19;

    [JsonProperty("height")]
    public int Height { get; set; } = 10;

    [JsonProperty("pick_station_id")]
    public int PickStationId { get; set; } = 1444;

    [JsonProperty("deliver_station_id")]
    public int? DeliverStationId { get; set; } = null;

    [JsonProperty("insurance_value")]
    public int InsuranceValue { get; set; } = 1000000;

    [JsonProperty("service_id")]
    public int ServiceId { get; set; } = 0;

    [JsonProperty("service_type_id")]
    public int ServiceTypeId { get; set; } = 2;

    [JsonProperty("coupon")]
    public string Coupon { get; set; } = null;

    [JsonProperty("pick_shift")]
    public List<int> PickShift { get; set; } = new List<int> { 2 };

    [JsonProperty("items")]
    public List<GhnOrderItem> Items { get; set; }
}

public class GhnOrderItem
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("quantity")]
    public int Quantity { get; set; }

    [JsonProperty("price")]
    public int Price { get; set; }

    [JsonProperty("length")]
    public int Length { get; set; }

    [JsonProperty("width")]
    public int Width { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("weight")]
    public int Weight { get; set; }

    [JsonProperty("category")]
    public GhnItemCategory Category { get; set; }
}

public class GhnItemCategory
{
    [JsonProperty("level1")]
    public string Level1 { get; set; }
}
