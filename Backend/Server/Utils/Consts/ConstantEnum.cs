namespace Server.Utils.Consts;

public static partial class ConstantEnum
{
    public enum UserRole
    {
        CUSTOMER,
        SELLER,
        ADMIN,
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