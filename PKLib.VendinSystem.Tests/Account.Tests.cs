using Microsoft.VisualStudio.TestTools.UnitTesting;
using PKLib.VendingSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PKLib.VendinSystem.Tests
{
    [TestClass]
   public class AccountTest
    {
        Account _accountObj;
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void Account_Will_Not_Be_Deducted_If_There_Is_No_Balance()
        {
            _accountObj = new Account(12345, 0); //initial balance set with .2
            Assert.AreEqual(-1, _accountObj.WithDrawAmount(0.25f));
        }

        [TestMethod]
        public void Account_Will_Be_Deducted_If_There_Is_Sufficient_Balance()
        {
            _accountObj = new Account(12345, 0.5f);
            Assert.AreEqual(0, _accountObj.WithDrawAmount(0.5f));
        }

        [TestMethod]
        public void Account_Will_Be_Deducted_If_There_Is_InSufficient_Balance()
        {
            _accountObj = new Account(12345, 0.2f);
            Assert.AreEqual(-1, _accountObj.WithDrawAmount(0.5f));
        }

        /// <summary>
        /// This tests covers scenario where account is accessed by two methods at the same time
        /// </summary>
        [TestMethod]
        public void Account_Accessed_By_Parallel_Threads_Should_Still_Show_Expected_Balance1()
        {
            _accountObj = new Account(12345, 10.5f);

            Parallel.Invoke
                (
                    () =>
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            _accountObj.WithDrawAmount(.5f);
                        }
                    },
                    () =>
                           {
                               for (int i = 0; i < 11; i++)
                               {
                                   _accountObj.WithDrawAmount(.5f);
                               }
                           }

                );

            Assert.AreEqual(0, _accountObj.Balance);


        }

        [TestMethod]
        public void Account_Accessed_By_Parallel_Threads_Should_Still_Show_Expected_Balance2()
        {
            _accountObj = new Account(12345, 10.5f);

            Parallel.Invoke
                (
                    () =>
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            _accountObj.WithDrawAmount(.5f);
                        }
                    },
                    () =>
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            _accountObj.WithDrawAmount(.5f);
                        }
                    }

                );

            Assert.AreEqual(5.5, _accountObj.Balance);


        }
    }
}
