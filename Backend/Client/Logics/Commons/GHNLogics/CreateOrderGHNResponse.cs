using Newtonsoft.Json;

namespace Client.Logics.Commons.GHNLogics;

public class CreateOrderGHNResponse
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("code_message_value")]
    public string CodeMessageValue { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("message_display")]
    public string MessageDisplay { get; set; }

    [JsonProperty("data")]
    public GhnOrderData Data { get; set; }
}

public class GhnOrderData
{
    [JsonProperty("order_code")]
    public string OrderCode { get; set; }

    [JsonProperty("sort_code")]
    public string SortCode { get; set; }

    [JsonProperty("trans_type")]
    public string TransType { get; set; }

    [JsonProperty("ward_encode")]
    public string WardEncode { get; set; }

    [JsonProperty("district_encode")]
    public string DistrictEncode { get; set; }

    [JsonProperty("total_fee")]
    public int TotalFee { get; set; }

    [JsonProperty("expected_delivery_time")]
    public DateTime ExpectedDeliveryTime { get; set; }

    [JsonProperty("operation_partner")]
    public string OperationPartner { get; set; }

    [JsonProperty("fee")]
    public GhnFee Fee { get; set; }
}

public class GhnFee
{
    [JsonProperty("main_service")]
    public int MainService { get; set; }

    [JsonProperty("insurance")]
    public int Insurance { get; set; }

    [JsonProperty("cod_fee")]
    public int CodFee { get; set; }

    [JsonProperty("station_do")]
    public int StationDo { get; set; }

    [JsonProperty("station_pu")]
    public int StationPu { get; set; }

    [JsonProperty("return")]
    public int Return { get; set; }

    [JsonProperty("r2s")]
    public int R2s { get; set; }

    [JsonProperty("return_again")]
    public int ReturnAgain { get; set; }

    [JsonProperty("coupon")]
    public int Coupon { get; set; }

    [JsonProperty("document_return")]
    public int DocumentReturn { get; set; }

    [JsonProperty("double_check")]
    public int DoubleCheck { get; set; }

    [JsonProperty("double_check_deliver")]
    public int DoubleCheckDeliver { get; set; }

    [JsonProperty("pick_remote_areas_fee")]
    public int PickRemoteAreasFee { get; set; }

    [JsonProperty("deliver_remote_areas_fee")]
    public int DeliverRemoteAreasFee { get; set; }

    [JsonProperty("pick_remote_areas_fee_return")]
    public int PickRemoteAreasFeeReturn { get; set; }

    [JsonProperty("deliver_remote_areas_fee_return")]
    public int DeliverRemoteAreasFeeReturn { get; set; }

    [JsonProperty("cod_failed_fee")]
    public int CodFailedFee { get; set; }
}