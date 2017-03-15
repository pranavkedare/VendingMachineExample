using PKLib.VendingSystem.Exceptions;
using System;
using System.Collections.Generic;

namespace PKLib.VendingSystem.Model
{
    //Instance of vending machine
    public  class VendingMachine : IVendingMachine
    {
        #region Member variables
        //As we load up vending machine from top down, LIFO.
        private Stack<VendingItem> _inventory;
        private const int systemCapacity = 25;
        private readonly IAccount _machineAccount;
        private ICardAuthority _cardAuthority;

        //System capacity constant.
        public static  int SystemCapacity => systemCapacity;

       
        

        #endregion

        /// <summary>
        /// Constructor initializes item stack.
        /// </summary>
        public VendingMachine()
        {
            _inventory = new Stack<VendingItem>();
        }

        /// <summary>
        /// Init machine account
        /// </summary>
        /// <param name="machineAccount"></param>
        public VendingMachine(IAccount machineAccount,ICardAuthority cardAuthority):
            this() //Also call default constructor
        {
            _machineAccount = machineAccount;
            _cardAuthority = cardAuthority;
        }

        /// <summary>
        /// Returns current inventory count
        /// </summary>
        /// <returns></returns>
        public int InventoryCount
        {
            get
            {
                return _inventory.Count;
            }
        }

        public float CurrentItemAmount
        {
            get
            {
                try
                {
                    var item = _inventory.Peek(); //If the following operation fails then the inventory is empty.
                    return item.Price;
                }
                catch (InvalidOperationException)
                {
                    throw new EmptyMachineException("Vending machine is out of stock, please try later");
                }
            }
        }

        /// <summary>
        /// Loads inventory items in existing inventory
        /// </summary>
        /// <param name="items">New items that are required to be loaded.</param>
        public void AddInventory(IEnumerable<VendingItem> items)
        {
            //Inventory should only load up to max capacity.
            if (items != null)
            {
                foreach (var item in items)
                {
                    if (InventoryCount< systemCapacity)
                    {
                        _inventory.Push(item);
                    }
                    else
                        break;

                }
            }
          
        }



        /// <summary>
        /// It serves Vending Item/Can if it is available.
        /// </summary>
        /// <returns></returns>
        public VendingItem VendIt()
        {
            try
            {
                var item = _inventory.Peek(); //If the following operation fails then the inventory is empty.
                return _inventory.Pop();
            }
            catch(InvalidOperationException e)
            {
                throw new EmptyMachineException("Vending machine is out of stock, please try later");
                

            }

            
        }

        /// <summary>
        /// Once the user card is debited, the vending account should get credited.
        /// </summary>
        /// <param name="transactionAmount"></param>
        public void CreditVendingAmount(float transactionAmount)
        {
            _machineAccount.DepositAmount(transactionAmount);
        }

        public ICard FindCard(string cardNumber)
        {
            return _cardAuthority.FindCard(cardNumber);
        }
    }
}
