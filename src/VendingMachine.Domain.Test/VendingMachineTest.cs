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
            var vendingMachine = CreateVendingMachine();
            vendingMachine.Restock(new DisplayProductNumber(1), 5);

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
            var vendingMachine = CreateVendingMachine();

            var product = vendingMachine.Purchase(new DisplayProductNumber(1));
            Assert.AreEqual(null, product);
        }

        [TestMethod]
        public void 購入後お釣りを返却する()
        {
            var vendingMachine = CreateVendingMachine();

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

        private VendingMachine CreateVendingMachine()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.SetProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("コーラ", new Price(100))));
            vendingMachine.SetProduct(new DisplayProduct(new DisplayProductNumber(2), new Product("オレンジジュース", new Price(100))));
            vendingMachine.SetProduct(new DisplayProduct(new DisplayProductNumber(3), new Product("緑茶", new Price(100))));
            vendingMachine.SetProduct(new DisplayProduct(new DisplayProductNumber(4), new Product("味噌汁", new Price(100))));
            vendingMachine.SetProduct(new DisplayProduct(new DisplayProductNumber(5), new Product("レッドブル", new Price(100))));

            vendingMachine.Restock(new DisplayProductNumber(1), 5);
            vendingMachine.Restock(new DisplayProductNumber(2), 5);
            vendingMachine.Restock(new DisplayProductNumber(3), 5);
            vendingMachine.Restock(new DisplayProductNumber(4), 5);
            vendingMachine.Restock(new DisplayProductNumber(5), 5);
            return vendingMachine;
        }
    }
}
