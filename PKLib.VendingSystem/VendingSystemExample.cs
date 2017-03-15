using PKLib.VendingSystem.Model;
using System;
using System.Collections.Generic;

namespace PKLib.VendingSystem
{
    public class VendingSystemExample
    {
        static void Main(string[] args)
        {

            VendingMachine vendingMachine=null;
            var vendingMachineAccount = new Account(54321, 0);
            BootUpVendingMachine(ref vendingMachine, vendingMachineAccount);


            //The same object will be used for two cards so we are creating a scenario of a joint card.
            var userCardAccount = new Account(123456, 25.5f);

            var cashCard1 = new CashCard("1234567891011123",4534,userCardAccount);

            var cashCard2 = new CashCard("1312111098765432", 4534, userCardAccount);
            bool _canRepeat=false;
            do
            {
               int userChoice = DisplayMenu();

                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("Machine has {0} cans", vendingMachine.GetInventoryCount());
                        _canRepeat = true;
                        break;
                    case 2:
                        _canRepeat = Transact(ref vendingMachine, ref cashCard1, _canRepeat);
                        break;
                    case 3:
                        _canRepeat = Transact(ref vendingMachine, ref cashCard2, _canRepeat);
                        break;
                    case 4:
                        _canRepeat = false;
                        break;
                    default:
                        _canRepeat = true;
                        Console.Write("Invalid input");
                        break;
                }

            } while (_canRepeat);
        }

        private static bool Transact(ref VendingMachine vendingMachine, ref CashCard cashCard, bool _canRepeat)
        {
            Console.WriteLine("Please Enter Pin for Card ending with {0}", cashCard.CardProtectedNumber);
            var input = Console.ReadLine();
            uint inputPin;
            if (UInt32.TryParse(input, out inputPin))
            {
                if (!cashCard.ValidateCard(inputPin))
                {
                    Console.WriteLine("Incorrect Pin!!");
                    _canRepeat = true;
                }
                else //Pin is correct.
                {
                    if (vendingMachine.GetInventoryCount() != 0)
                    {
                        //1.Deduct the amount from the card.
                        //2.Get the item amount
                        if (vendingMachine.GetCurrentItemAmount().HasValue &&
                            cashCard.DebitAmount(vendingMachine.GetCurrentItemAmount().Value))
                        {
                            var transAmount = vendingMachine.GetCurrentItemAmount().Value;
                            //3.Credit Vending machine account
                            vendingMachine.CreditAmount(transAmount);
                            //4. Serve the item out.
                            var item = vendingMachine.VendIt();
                            Console.WriteLine("Your card ending with {0} has been deducted with {1}",
                                cashCard.CardProtectedNumber, transAmount);
                            Console.WriteLine("Please collect your {0}", item.Name);
                            _canRepeat = true;
                        }
                        else
                        {
                            Console.WriteLine("Insufficient funds on your account,please check.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Machine is empty, please try later");
                    }
                }

            }

            return _canRepeat;
        }

        /// <summary>
        /// 1. Init Vending machine.
        /// 2. Set the account to operate/
        /// 3. Add initial inventory.
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="machineAccount"></param>
        private static void BootUpVendingMachine(ref VendingMachine machine,Account machineAccount)
        {
            machine = new VendingMachine(machineAccount);

            List<VendingItem> items = new List<VendingItem>();
            for(int iVar = 1; iVar <= VendingMachine.SystemCapacity; iVar++)
            {
                items.Add(new VendingItem { Name = string.Format("Can {0}", iVar), Price = 0.5f });
            }
            machine.AddInventory(items);
        }

        static public int DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Vending system");
            Console.WriteLine();
            Console.WriteLine("1. Check Vending machine stock count");
            Console.WriteLine("2. Use Card 1 for a Can");
            Console.WriteLine("3. Use Card 2 for a Can");
            Console.WriteLine("4. Exit");
            var result = Console.ReadLine();
            int parseInt=-1;
             Int32.TryParse(result,out parseInt);
            return parseInt;
        }
    }
}
