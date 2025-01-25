namespace Server.Utils.Consts;

public static partial class ConstantEnum
{
    public enum UserRole
    {
        Customer,
        Admin,
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