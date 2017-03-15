using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PKLib.VendingSystem.Model;
using PKLib.VendingSystem.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PKLib.VendinSystem.Tests
{
    [TestClass]
    public class CardTest
    {
        ICard card1, card2;
        IAccount jointAccount;
        [TestInitialize]
        public void Init()
        {
            //The same object will be used for two cards so we are creating a scenario of a joint card.
            jointAccount = new Account(123456, 10f);

            //Card number should be ideally digits but for card recognition simulation, 
            //we will make it four digits card.
            //Card 1
          
            card1 = new CashCard("4512",2535);
            card1.LinkAccount(jointAccount); //Registering same account to both cards.

            //Card 1
            card2 = new CashCard("4513", 2535);           
            card2.LinkAccount(jointAccount);
        }

        [TestMethod]
        public void Pin_Valid_Should_Return_True()
        {
            Assert.IsTrue(card1.ValidatePin(2535));
        }

        [TestMethod]
        public void Pin_Invalid_Should_Throw_Exception()
        {
            Assert.ThrowsException<InvalidPinException>( ()=>card2.ValidatePin(1111));
        }

        [TestMethod]
        public void Pin_Set_Will_Change_Pin_Value()
        {
            card2.SetPin(2536);
            Assert.IsTrue(card2.ValidatePin(2536));
        }

        [TestMethod]
        public void Both_Card_Accessed__Same_Time_Will_Reduce_Linked_Account_Balance()
        {
            var tasks = new List<Task>();
            for(int i=0; i < 10;i++)
            {
                if(i%2==0)
                {
                    tasks.Add(Task.Factory.StartNew(()=>card2.DebitAmount(.5f)));
                }
                else
                    tasks.Add(Task.Factory.StartNew(() => card1.DebitAmount(.5f)));
            }
            Task.WaitAll(tasks.ToArray());

            Assert.AreEqual(5, jointAccount.Balance); //Init balance 10 , .5 * 5 + .5 * 5
        }

    }
}
