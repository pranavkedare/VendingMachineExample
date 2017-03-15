namespace PKLib.VendingSystem.Model
{
    public interface IAccount
    {
        float Balance { get; }
        float WithDrawAmount(float transactionAmount);
        float DepositAmount(float transactionAmount);
    }
}
