namespace PKLib.VendingSystem.Model
{
    public interface ICard
    {
        bool ValidatePin(uint pin);
        void SetPin(uint pin);
        void LinkAccount(IAccount account);
        bool DebitAmount(float transactionAmount);
    }
}
