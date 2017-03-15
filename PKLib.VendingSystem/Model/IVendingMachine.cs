using System.Collections.Generic;

namespace PKLib.VendingSystem.Model
{
    public interface IVendingMachine
    {
        int InventoryCount { get; }
        float CurrentItemAmount { get; }
        void AddInventory(IEnumerable<VendingItem> items);
        VendingItem VendIt();
        void CreditVendingAmount(float transactionAmount);
        ICard FindCard(string cardNumber);
    }
}
