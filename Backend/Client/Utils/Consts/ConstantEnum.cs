namespace Client.Utils.Consts;

public static class ConstantEnum
{
    /// <summary>
    /// Type of product. BlindBox or Accessory
    /// </summary>
    public enum ProductType
    {
        Product = 1,
        BlindBox = 2,
    }
    
    /// <summary>
    /// Sort type
    /// </summary>
    public enum Sort
    {
        MostPopular = 1,
        DecreasingPrice = 2,
        IncreasingPrice = 3,	
        Newest = 4,
        Oldest = 5,
    }
    
    /// <summary>
    /// Status of Exchange
    /// </summary>
    public enum ExchangeStatus
    {
        PendingExchange = 1,
        Approve = 2,
        Success = 3,
        Fail = 4
    }

    public enum PostingStatus
    {
        PendingExchange = 1,
        Fail = 2,
        PendingRecheck = 3,
    }
    
    /// <summary>
    /// Status of Order Plan
    /// </summary>
    public enum OrderPlans
    {
        Success, 
        Fail, 
        Pending, 
        Cancel, 
        Refunded
    }
    
    /// <summary>
    /// Status of Order
    /// </summary>
    public enum OrderStatus
    {
        Pending = 1,
        Processing = 2,
        Shipping = 3,
        Delivered = 4,
        Cancel = 5,
        Refunded = 6
    }
    
    /// <summary>
    /// Payment method
    /// </summary>
    public enum PaymentMethod
    {
        Online = 1,
        CashOnDelivery = 2
    }
}