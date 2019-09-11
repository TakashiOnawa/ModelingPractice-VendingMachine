using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class DisplayProducts
    {
        private readonly ConcurrentDictionary<DisplayProductNumber, DisplayProduct> _displayProducts = new ConcurrentDictionary<DisplayProductNumber, DisplayProduct>();
        private readonly int _maxProductCount;

        public DisplayProducts(int maxProductCount)
        {
            if (maxProductCount < 1) throw new ArgumentException(nameof(maxProductCount) + " is zero or negative value.");
            _maxProductCount = maxProductCount;
        }

        public void AddOrUpdate(DisplayProduct displayProduct)
        {
            if (!_displayProducts.ContainsKey(displayProduct.ProductNumber) && _displayProducts.Count >= _maxProductCount)
                throw new InvalidOperationException("Products count is over maximum.");

            _displayProducts.AddOrUpdate(displayProduct.ProductNumber, displayProduct, (key, value) => displayProduct);
        }

        public DisplayProduct Find(DisplayProductNumber displayProductNumber)
        {
            _displayProducts.TryGetValue(displayProductNumber, out var displayProduct);
            return displayProduct;
        }

        public DisplayProduct FindWithValidation(DisplayProductNumber displayProductNumber)
        {
            var displayProduct = Find(displayProductNumber);
            if (displayProduct == null) throw new InvalidOperationException("DisplayProduct is not exists.");
            return displayProduct;
        }
    }
}
