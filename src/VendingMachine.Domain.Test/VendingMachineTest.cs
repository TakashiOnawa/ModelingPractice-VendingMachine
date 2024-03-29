using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        public void 入金額が足りない場合は購入できない()
        {
            var vendingMachine = CreateVendingMachine();

            var product = vendingMachine.Purchase(new DisplayProductNumber(1));
            Assert.AreEqual(null, product);
        }

        [TestMethod]
        public void お釣りが足りない場合は購入できない()
        {
            var vendingMachine = CreateVendingMachine();

            vendingMachine.Post(Money._10);
            vendingMachine.Post(Money._500);
            vendingMachine.Post(Money._1000);

            var product = vendingMachine.Purchase(new DisplayProductNumber(1));
            Assert.AreEqual(null, product);
        }

        [TestMethod]
        public void 売り切れの場合購入できない()
        {
            var vendingMachine = CreateVendingMachine();
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("コーラ", new Price(100))));

            vendingMachine.Post(Money._100);

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

        [TestMethod]
        public void 使用可能なお金のみ投入できる()
        {
            var vendingMachine = CreateVendingMachine();

            Assert.IsFalse(vendingMachine.Post(Money._1));
            Assert.IsFalse(vendingMachine.Post(Money._5));
            Assert.IsTrue(vendingMachine.Post(Money._10));
            Assert.IsTrue(vendingMachine.Post(Money._100));
            Assert.IsTrue(vendingMachine.Post(Money._500));
            Assert.IsFalse(vendingMachine.Post(Money._2000));
            Assert.IsFalse(vendingMachine.Post(Money._5000));
            Assert.IsFalse(vendingMachine.Post(Money._10000));
        }

        [TestMethod]
        public void 上限枚数以上のお金は投入できない()
        {
            var vendingMachine = CreateVendingMachine();

            void TestPost(Money money, int maxPostableCount)
            {
                for (int postCount = 0; postCount < maxPostableCount; postCount++)
                    Assert.IsTrue(vendingMachine.Post(money));

                Assert.IsFalse(vendingMachine.Post(money));
            }

            TestPost(Money._10, 30);
            TestPost(Money._100, 30);
            TestPost(Money._500, 30);
            TestPost(Money._1000, 30);
        }

        [TestMethod]
        public void 商品数は50個まで()
        {
            var vendingMachine = new VendingMachine();

            for (int productCount = 0; productCount < 50; productCount++)
            {
                var number = productCount + 1;
                vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(number), new Product("テスト" + number, new Price(100))));
            }

            Assert.ThrowsException<InvalidOperationException>(() => 
            {
                vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(51), new Product("テスト" + 51, new Price(100))));
            });
        }

        [TestMethod]
        public void 商品の値段は10円以上300円以下でお釣りが返せる値段である()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("テスト", new Price(10))));
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("テスト", new Price(300))));

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("テスト", new Price(0))));
            });
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("テスト", new Price(105))));
            });
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("テスト", new Price(310))));
            });
        }

        private VendingMachine CreateVendingMachine()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(1), new Product("コーラ", new Price(100))));
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(2), new Product("オレンジジュース", new Price(100))));
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(3), new Product("緑茶", new Price(100))));
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(4), new Product("味噌汁", new Price(100))));
            vendingMachine.SetDisplayProduct(new DisplayProduct(new DisplayProductNumber(5), new Product("レッドブル", new Price(100))));

            vendingMachine.RestockProduct(new DisplayProductNumber(1), new ProductStockQuantity(5));
            vendingMachine.RestockProduct(new DisplayProductNumber(2), new ProductStockQuantity(5));
            vendingMachine.RestockProduct(new DisplayProductNumber(3), new ProductStockQuantity(5));
            vendingMachine.RestockProduct(new DisplayProductNumber(4), new ProductStockQuantity(5));
            vendingMachine.RestockProduct(new DisplayProductNumber(5), new ProductStockQuantity(5));
            return vendingMachine;
        }
    }
}
