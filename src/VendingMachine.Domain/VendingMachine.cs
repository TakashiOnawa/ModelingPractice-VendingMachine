using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine.Domain
{
    public class VendingMachine
    {
        private readonly DisplayProducts _displayProducts = new DisplayProducts();
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
            _displayProducts.SetDisplayProduct(displayProduct);
        }

        public void RestockChange(Money money, int restockCount)
        {
            for (int i = 0; i < restockCount; i++)
                _deposit.RestockChange(money);
        }

        public void RestockProduct(DisplayProductNumber displayProductNumber, ProductStockQuantity salableStock)
        {
            _displayProducts.Restock(displayProductNumber, salableStock);
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
