using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace VendingMachine.Domain.Test
{
    [TestClass]
    public class VendingMachineTest
    {
        [TestMethod]
        public void 指定された番号の商品を購入できる()
        {
            var vendingMachine = VendingMachine.Create();

            vendingMachine.Post(Money._100);
            var product = vendingMachine.Purchase(new DisplayProductNumber(1));
            Assert.AreEqual("コーラ", product.Name);

            vendingMachine.Post(Money._100);
            product = vendingMachine.Purchase(new DisplayProductNumber(2));
            Assert.AreEqual("オレンジジュース", product.Name);

            vendingMachine.Post(Money._100);
            product = vendingMachine.Purchase(new DisplayProductNumber(3));
            Assert.AreEqual("緑茶", product.Name);

            vendingMachine.Post(Money._100);
            product = vendingMachine.Purchase(new DisplayProductNumber(4));
            Assert.AreEqual("味噌汁", product.Name);

            vendingMachine.Post(Money._100);
            product = vendingMachine.Purchase(new DisplayProductNumber(5));
            Assert.AreEqual("レッドブル", product.Name);
        }

        [TestMethod]
        public void 入金額が足りていない場合は購入できない()
        {
            var vendingMachine = VendingMachine.Create();

            var product = vendingMachine.Purchase(new DisplayProductNumber(1));
            Assert.AreEqual(null, product);
        }

        [TestMethod]
        public void 購入後お釣りを返却する()
        {
            var vendingMachine = VendingMachine.Create();

            vendingMachine.Post(Money._100);
            vendingMachine.Purchase(new DisplayProductNumber(1));
            var change = vendingMachine.Refund();
            Assert.AreEqual(0, change.Count());

            vendingMachine.Post(Money._100);
            vendingMachine.Post(Money._100);
            vendingMachine.Purchase(new DisplayProductNumber(1));
            change = vendingMachine.Refund();
            Assert.AreEqual(1, change.Count());
            Assert.AreEqual(Money._100, change.ElementAt(0));
        }
    }
}
