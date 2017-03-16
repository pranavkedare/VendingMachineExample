using Microsoft.VisualStudio.TestTools.UnitTesting;
using PKLib.VendingSystem.Exceptions;
using PKLib.VendingSystem.Model;
using System.Collections.Generic;

namespace PKLib.VendingSystem.Tests
{
    [TestClass]
    public class VendingMachineTest
    {
        IVendingMachine _machine;

        [TestInitialize]
        public void Setup()
        {
            _machine = new VendingMachine();
        }

        [TestMethod]
        public void No_Inventory_At_Start_Up()
        {
            Assert.AreEqual(0, _machine.InventoryCount);
        }

        [TestMethod]
        public void Inventory_Can_Only_Filled_Upto_Capacity()
        {
            Assert.IsTrue(0 == _machine.InventoryCount);
            AddSomeItemsToInventory();

            Assert.IsTrue(_machine.InventoryCount== VendingMachine.SystemCapacity);
        }

        private void AddSomeItemsToInventory()
        {
            var vendingItems = new List<VendingItem>();

            for (int i = 1; i < 100; i++)
            {
                vendingItems.Add(new VendingItem { Name = string.Format("Can{0}", i), Price = 0.5f });
            }
            _machine.AddInventory(vendingItems);
        }

        [TestMethod]
        public void Inventory_Will_Not_Vend_AnyThing_If_Empty()
        {
            Assert.IsTrue(0 == _machine.InventoryCount);
            Assert.ThrowsException<EmptyMachineException>(()=>_machine.CurrentItemAmount);
        }

        [TestMethod]
        public void Inventory_Will_Vend_AnyThing_If_Not_Empty()
        {
            Assert.IsTrue(0 == _machine.InventoryCount);
            AddSomeItemsToInventory();
            Assert.IsNotNull(_machine.VendIt());
        }
    }
}
