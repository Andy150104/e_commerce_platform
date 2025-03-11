namespace Client.Logics.Commons.GHNLogics;

public class CreateOrderGHNRequest
{
    public int PaymentTypeId { get; set; } = 2;
    public string Note { get; set; } = "Hàng dễ vỡ";
    public string RequiredNote { get; set; } = "KHONGCHOXEMHANG";

    // Info of sender
    public string FromName { get; set; } = "BlindBag Ecommerce and Trade";
    public string FromPhone { get; set; } = "0987654321";
    public string FromAddress { get; set; } = "FPTU D1";
    public string FromWardName { get; set; } = "Phường Tăng Nhơn Phú A";
    public string FromDistrictName { get; set; } = "Quận 9";
    public string FromProvinceName { get; set; } = "HCM";

    // Info of receiver
    public string ToName { get; set; }
    public string ToPhone { get; set; }
    public string ToAddress { get; set; }
    public string ToWardCode { get; set; }
    public int ToDistrictId { get; set; }

    // Inf of return
    public string ReturnPhone { get; set; } = "0332190444";
    public string ReturnAddress { get; set; } = "39 NTT";
    public int? ReturnDistrictId { get; set; } = null;
    public string ReturnWardCode { get; set; } = "70000";

    public string ClientOrderCode { get; set; } = "";
    public int CodAmount { get; set; } = 200000;
    public string Content { get; set; } = "Theo New York Times";

    public int Weight { get; set; } = 200;
    public int Length { get; set; } = 1;
    public int Width { get; set; } = 19;
    public int Height { get; set; } = 10;

    public int PickStationId { get; set; } = 1444;
    public int? DeliverStationId { get; set; } = null;

    public int InsuranceValue { get; set; } = 1000000;
    public int ServiceId { get; set; } = 0;
    public int ServiceTypeId { get; set; } = 2;
    public string Coupon { get; set; } = null;


    public List<int> PickShift { get; set; } = new List<int> { 2 };

    public List<GhnOrderItem> Items { get; set; }
}

public class GhnOrderItem
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public GhnItemCategory Category { get; set; }
}

public class GhnItemCategory
{
    public string Level1 { get; set; }
}