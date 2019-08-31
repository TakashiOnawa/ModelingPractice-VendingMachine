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

        public void SetDisplayProduct(DisplayProduct displayProduct)
        {
            _displayProducts.SetDisplayProduct(displayProduct);
        }

        public void RestockChange(IEnumerable<Money> change)
        {
            _deposit.Restock(change);
        }

        public void RestockProduct(DisplayProductNumber displayProductNumber, int salableStock)
        {
            _displayProducts.Restock(displayProductNumber, salableStock);
        }

        public void Post(Money money)
        {
            _deposit.Post(money);
        }

        public Product Purchase(DisplayProductNumber displayProductNumber)
        {
            var displayProduct = _displayProducts.FindWithValidate(displayProductNumber);

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
            var displayProduct = _displayProducts.FindWithValidate(displayProductNumber);

            if (displayProduct.SoldOut)
                return SalesStatus.SoldOut;
            if (_deposit.CanPurches(displayProduct.DisplayPrice))
                return SalesStatus.Salable;

            return SalesStatus.Unsalable;
        }
    }
}
