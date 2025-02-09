namespace Server.Utils.Consts;

public static partial class ConstantEnum
{
    public enum UserRole
    {
        CUSTOMER,
        SALE_EMPLOYEE,
        PLANNED_CUSTOMER,	
        OWNER,
    }
    
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled,
    }
    
    public enum PaymentMethod
    {
        Cash,
        CreditCard,
        DebitCard,
        BankTransfer,
    }

}