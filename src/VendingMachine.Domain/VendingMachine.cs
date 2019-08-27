using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine.Domain
{
    public class VendingMachine
    {
        private readonly Dictionary<DisplayProductNumber, DisplayProduct> _displayProducts = new Dictionary<DisplayProductNumber, DisplayProduct>();
        private readonly Deposit _deposit = new Deposit();

        public static VendingMachine Create()
        {
            var vendingMachine = new VendingMachine();
            var displayProductNumber = new DisplayProductNumber(1);
            vendingMachine._displayProducts.Add(displayProductNumber, new DisplayProduct(displayProductNumber, new Product("コーラ", new Price(100))));
            displayProductNumber = new DisplayProductNumber(2);
            vendingMachine._displayProducts.Add(displayProductNumber, new DisplayProduct(displayProductNumber, new Product("オレンジジュース", new Price(100))));
            displayProductNumber = new DisplayProductNumber(3);
            vendingMachine._displayProducts.Add(displayProductNumber, new DisplayProduct(displayProductNumber, new Product("緑茶", new Price(100))));
            displayProductNumber = new DisplayProductNumber(4);
            vendingMachine._displayProducts.Add(displayProductNumber, new DisplayProduct(displayProductNumber, new Product("味噌汁", new Price(100))));
            displayProductNumber = new DisplayProductNumber(5);
            vendingMachine._displayProducts.Add(displayProductNumber, new DisplayProduct(displayProductNumber, new Product("レッドブル", new Price(100))));
            return vendingMachine;
        }

        public void Post(Money money)
        {
            _deposit.Post(money);
        }

        public Product Purchase(DisplayProductNumber displayProductNumber)
        {
            if (!_displayProducts.TryGetValue(displayProductNumber, out var displayProduct))
                return null;

            if (!displayProduct.CanPurchase(_deposit))
                return null;

            var product = displayProduct.Purchase();

            _deposit.StorePurchesdAmount(displayProduct.Price);

            return product;
        }

        public IEnumerable<Money> Refund()
        {
            return _deposit.Refund();
        }
    }
}
