using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKLib.VendingSystem.Model
{
    //Instance of vending machine
    public sealed class VendingMachine
    {
        #region Member variables
        //As we load up vending machine from top down, LIFO.
        private Stack<VendingItem> _inventory;
        private const int systemCapacity = 25;
        private readonly Account _machineAccount;

        public static int SystemCapacity => systemCapacity;
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
        public VendingMachine(Account machineAccount):this() //Akso call default constructor
        {
            _machineAccount = machineAccount;
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
                    if (GetInventoryCount() < systemCapacity)
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
                Console.WriteLine("Machine has run out of Cans, please come back later.");
                return null;

            }

            
        }

        public void CreditAmount(float transactionAmount)
        {
            _machineAccount.DepositAmount(transactionAmount);
        }

        /// <summary>
        /// Returns current inventory count
        /// </summary>
        /// <returns></returns>
        public int GetInventoryCount()
        {
            return _inventory.Count;
        }

        public float? GetCurrentItemAmount()
        {
           try
            {
                var item = _inventory.Peek(); //If the following operation fails then the inventory is empty.
                return item.Price;
            }
            catch(InvalidOperationException e)
            {
                return null;
            }
        }

    }
}
