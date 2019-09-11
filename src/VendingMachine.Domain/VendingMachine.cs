using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine.Domain
{
    public class VendingMachine
    {
        private readonly DisplayProducts _displayProducts = new DisplayProducts(50);
        private readonly Deposit _deposit = new Deposit();

        public VendingMachine()
        {
            _deposit.SetMoneyStock(new MoneyStock(Money._10, 30));
            _deposit.SetMoneyStock(new MoneyStock(Money._100, 30));
            _deposit.SetMoneyStock(new MoneyStock(Money._500, 30));
            _deposit.SetMoneyStock(new MoneyStock(Money._1000, 30));
        }

        public void SetDisplayProduct(DisplayProduct displayProduct)
        {
            var minPostableMoney = _deposit.PostableMoney.Min();
            if (displayProduct.DisplayPrice.Value % minPostableMoney.Value != 0 ||
                displayProduct.DisplayPrice.Value < minPostableMoney.Value ||
                displayProduct.DisplayPrice.Value < 10 ||
                displayProduct.DisplayPrice.Value > 300)
                throw new InvalidOperationException("Can't set the price.");
            _displayProducts.AddOrUpdate(displayProduct);
        }

        public void RestockChange(Money money, int restockCount)
        {
            for (int i = 0; i < restockCount; i++)
                _deposit.RestockChange(money);
        }

        public void RestockProduct(DisplayProductNumber displayProductNumber, ProductStockQuantity salableStock)
        {
            var displayProduct = _displayProducts.FindWithValidation(displayProductNumber);
            displayProduct.Restock(salableStock);
        }

        public bool Post(Money money)
        {
            if (!_deposit.CanPost(money))
                return false;

            _deposit.Post(money);

            return true;
        }

        public Product Purchase(DisplayProductNumber displayProductNumber)
        {
            var displayProduct = _displayProducts.FindWithValidation(displayProductNumber);

            if (displayProduct.SoldOut)
                return null;

            if (!_deposit.CanPurches(displayProduct.DisplayPrice))
                return null;

            _deposit.StorePurchesdAmount(displayProduct.DisplayPrice);

            return displayProduct.Purchase();
        }

        public IEnumerable<Money> Refund()
        {
            return _deposit.Refund();
        }

        public SalesStatus GetSalesStatus(DisplayProductNumber displayProductNumber)
        {
            var displayProduct = _displayProducts.FindWithValidation(displayProductNumber);

            if (displayProduct.SoldOut)
                return SalesStatus.SoldOut;
            if (_deposit.CanPurches(displayProduct.DisplayPrice))
                return SalesStatus.Salable;

            return SalesStatus.Unsalable;
        }
    }
}
