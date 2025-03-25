using Newtonsoft.Json;

namespace Client.Controllers.V1.Orders;

public class TrackingGhnOrderResponse : AbstractApiResponse<GhnOrderResponse>
{
    public override GhnOrderResponse Response { get; set; }
}
public class GhnOrderResponse
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public GhnOrderData Data { get; set; }
}

public class GhnOrderData
{
    [JsonProperty("return_name")]
    public string ReturnName { get; set; }

    [JsonProperty("return_phone")]
    public string ReturnPhone { get; set; }

    [JsonProperty("return_address")]
    public string ReturnAddress { get; set; }

    [JsonProperty("return_ward_code")]
    public string ReturnWardCode { get; set; }

    [JsonProperty("return_district_id")]
    public int ReturnDistrictId { get; set; }

    [JsonProperty("return_location")]
    public GhnLocation ReturnLocation { get; set; }

    [JsonProperty("from_name")]
    public string FromName { get; set; }

    [JsonProperty("from_phone")]
    public string FromPhone { get; set; }

    [JsonProperty("from_address")]
    public string FromAddress { get; set; }

    [JsonProperty("from_ward_code")]
    public string FromWardCode { get; set; }

    [JsonProperty("from_district_id")]
    public int FromDistrictId { get; set; }

    [JsonProperty("from_location")]
    public GhnLocation FromLocation { get; set; }

    [JsonProperty("to_name")]
    public string ToName { get; set; }

    [JsonProperty("to_phone")]
    public string ToPhone { get; set; }

    [JsonProperty("to_address")]
    public string ToAddress { get; set; }

    [JsonProperty("to_ward_code")]
    public string ToWardCode { get; set; }

    [JsonProperty("to_district_id")]
    public int ToDistrictId { get; set; }

    [JsonProperty("to_location")]
    public GhnLocation ToLocation { get; set; }

    [JsonProperty("weight")]
    public int Weight { get; set; }

    [JsonProperty("length")]
    public int Length { get; set; }

    [JsonProperty("width")]
    public int Width { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("cod_amount")]
    public int CodAmount { get; set; }

    [JsonProperty("insurance_value")]
    public int InsuranceValue { get; set; }

    [JsonProperty("required_note")]
    public string RequiredNote { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("items")]
    public List<GhnOrderItem> Items { get; set; }

    [JsonProperty("order_code")]
    public string OrderCode { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("leadtime_order")]
    public GhnLeadTime LeadtimeOrder { get; set; }
}

public class GhnLocation
{
    [JsonProperty("lat")]
    public double Lat { get; set; }

    [JsonProperty("long")]
    public double Long { get; set; }

    [JsonProperty("cell_code")]
    public string CellCode { get; set; }

    [JsonProperty("place_id")]
    public string PlaceId { get; set; }

    [JsonProperty("trust_level")]
    public int TrustLevel { get; set; }

    [JsonProperty("wardcode")]
    public string Wardcode { get; set; }

    [JsonProperty("map_source")]
    public string MapSource { get; set; }
}

public class GhnOrderItem
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("quantity")]
    public int Quantity { get; set; }

    [JsonProperty("length")]
    public int Length { get; set; }

    [JsonProperty("width")]
    public int Width { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("weight")]
    public int Weight { get; set; }

    [JsonProperty("item_order_code")]
    public string ItemOrderCode { get; set; }
}

public class GhnLeadTime
{
    [JsonProperty("from_estimate_date")]
    public string FromEstimateDate { get; set; }

    [JsonProperty("to_estimate_date")]
    public string ToEstimateDate { get; set; }
}