using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class VendingMachine
    {
        private readonly List<DisplayProduct> _displayProducts = new List<DisplayProduct>();
        private readonly List<Money> _postedMoney = new List<Money>();

        public static VendingMachine Create()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine._displayProducts.Add(new DisplayProduct(new DisplayProductNumber(1), new Product("コーラ", new Price(100))));
            vendingMachine._displayProducts.Add(new DisplayProduct(new DisplayProductNumber(1), new Product("オレンジジュース", new Price(100))));
            vendingMachine._displayProducts.Add(new DisplayProduct(new DisplayProductNumber(1), new Product("緑茶", new Price(100))));
            vendingMachine._displayProducts.Add(new DisplayProduct(new DisplayProductNumber(1), new Product("味噌汁", new Price(100))));
            vendingMachine._displayProducts.Add(new DisplayProduct(new DisplayProductNumber(1), new Product("レッドブル", new Price(100))));
            return vendingMachine;
        }

        public void Post(Money money)
        {
            throw new NotImplementedException();
        }

        public Product Purchase(DisplayProductNumber displayProductNumber)
        {
            throw new NotImplementedException();
        }

        public List<Money> Refund()
        {
            throw new NotImplementedException();
        }
    }
}
