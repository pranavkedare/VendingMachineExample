namespace PKLib.VendingSystem.Model
{
    public interface ICardAuthority
    {
        ICard FindCard(string cardNumber);
        void RegisterCard(string cardNumber);
    }
}
