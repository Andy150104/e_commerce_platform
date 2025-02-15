namespace Server.Utils.Consts;

public static partial class ConstantEnum
{
    public enum UserRole
    {
        Customer,
        SaleEmployee,
        PlannedCustomer,	
        Owner,
    }
    
    public enum Result
    {
        Success = 1,
        Failure = 0,
    }
}