namespace Client.Utils.Consts;

public static class ConstantEnum
{
    /// <summary>
    /// Type of product. BlindBox or Product
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
    /// Status of BlindBox
    /// </summary>
    public enum Status
    {
        Success = 1,
        Pending = 2,
        Fail = 3
    }


}