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

        public void SetProduct(DisplayProduct displayProduct)
        {
            _displayProducts.SetDisplayProduct(displayProduct);
        }

        public void Restock(DisplayProductNumber displayProductNumber, int salableStock)
        {
            _displayProducts.Restock(displayProductNumber, salableStock);
        }

        public void Post(Money money)
        {
            _deposit.Post(money);
        }

        public Product Purchase(DisplayProductNumber displayProductNumber)
        {
            var displayProduct = _displayProducts.Find(displayProductNumber);

            if (displayProduct == null)
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
    }
}
