using PKLib.VendingSystem.Model;
using System;
using System.Collections.Generic;

namespace PKLib.VendingSystem
{
    public class VendingSystemExample
    {
        static IVendingMachine _vendingMachineInstance;
        static void Main(string[] args)
        {
            ICardAuthority cardAuthority = new CardAuthority();
            IAccount vendingMachineAccount = new Account(54321, 0);
            _vendingMachineInstance = new VendingMachine(vendingMachineAccount, cardAuthority);

            BootUpVendingMachine();//Adding items to vending machine

            UserCardRegistration(cardAuthority); //registering joint cards to same account

            Console.WriteLine("===========================");
            Console.WriteLine("Welcome to vending system");
            Console.WriteLine("===========================");

            Console.Write("Insert card and enter card number: "); //It is just a simulation of card recognition.
            var result = Console.ReadLine();
            Transact(result);

           
        }

        private static void UserCardRegistration(ICardAuthority cardAuthority)
        {
            //The same object will be used for two cards so we are creating a scenario of a joint card.
            var userCardAccount = new Account(123456, 25.5f);

            //Card number should be ideally 16 digits but for card recognition simulation, 
            //we will make it four digits card.
            //Card 1
            cardAuthority.RegisterCard("4512");
            var cashCard1 = cardAuthority.FindCard("4512");

            cashCard1.SetPin(2535);
            cashCard1.LinkAccount(userCardAccount); //Registering same account to both cards.

            //Card 1
            cardAuthority.RegisterCard("4513");
            var cashCard2 = cardAuthority.FindCard("4513");

            cashCard2.SetPin(2535);
            cashCard2.LinkAccount(userCardAccount);

            
        }

        private static void Transact(string cardNumber)
        {
            try
            {


                var transactionAmount = _vendingMachineInstance.CurrentItemAmount;
                Console.WriteLine("Card inserted");
                var cardToTransact = _vendingMachineInstance.FindCard(cardNumber);
                Console.Write("Please enter Pin: ");
                var input = Console.ReadLine();
                uint inputPin;
                if (UInt32.TryParse(input, out inputPin))
                {
                    if (cardToTransact.ValidatePin(inputPin)) //Pin is correct
                    {

                        //1.Deduct the amount from users' card.

                        if (!cardToTransact.DebitAmount(transactionAmount))
                            throw new InsufficientExecutionStackException("Insufficient funds on your account.");

                        //2.Credit Vending machine account
                        _vendingMachineInstance.CreditVendingAmount(transactionAmount);

                        //3. Serve the vending item out.
                        var item = _vendingMachineInstance.VendIt();
                        Console.WriteLine("Your card has been deducted with {0}", transactionAmount);
                        Console.WriteLine("Please collect your {0}.Thank you", item.Name);


                    }

                }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Please start again.");
               
            }
            Console.Read();
        }

        /// <summary>
        ///  Add initial inventory.
        /// </summary>
        
        private static void BootUpVendingMachine()
        { 

            List<VendingItem> items = new List<VendingItem>();
            for(int iVar = 1; iVar <= VendingMachine.SystemCapacity; iVar++)
            {
                items.Add(new VendingItem { Name = string.Format("Can {0}", iVar), Price = 0.5f });
            }
            _vendingMachineInstance.AddInventory(items);
        }

       
    }
}
