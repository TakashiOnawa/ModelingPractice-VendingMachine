using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class DisplayProducts
    {
        private readonly ConcurrentDictionary<DisplayProductNumber, DisplayProduct> _displayProducts = new ConcurrentDictionary<DisplayProductNumber, DisplayProduct>();

        public void SetDisplayProduct(DisplayProduct displayProduct)
        {
            _displayProducts.AddOrUpdate(displayProduct.ProductNumber, displayProduct, (key, value) => displayProduct);
        }

        public DisplayProduct Find(DisplayProductNumber displayProductNumber)
        {
            _displayProducts.TryGetValue(displayProductNumber, out var displayProduct);
            return displayProduct;
        }

        public void Restock(DisplayProductNumber displayProductNumber, int salableStock)
        {
            var displayProduct = Find(displayProductNumber);
            if (displayProduct == null)
                return;

            displayProduct.Restock(salableStock);
        }
    }
}
