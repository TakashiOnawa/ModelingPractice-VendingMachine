using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace VendingMachine.Domain.Test
{
    [TestClass]
    public class MoneyTest
    {
        [TestMethod]
        public void Moneyの各インスタンスが生成できる()
        {
            Assert.AreEqual(1, Money._1.Value);
            Assert.AreEqual(10, Money._10.Value);
            Assert.AreEqual(50, Money._50.Value);
            Assert.AreEqual(100, Money._100.Value);
            Assert.AreEqual(500, Money._500.Value);
            Assert.AreEqual(1000, Money._1000.Value);
            Assert.AreEqual(2000, Money._2000.Value);
            Assert.AreEqual(5000, Money._5000.Value);
            Assert.AreEqual(10000, Money._10000.Value);
        }

        [TestMethod]
        public void Moneyの比較ができる()
        {
            Assert.IsTrue(Money._1 != Money._10);
        }

    }
}
