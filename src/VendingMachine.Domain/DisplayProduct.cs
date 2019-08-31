using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class DisplayProduct
    {
        public DisplayProduct(DisplayProductNumber productNumber, Product product)
        {
            ProductNumber = productNumber;
            Product = product;
            DisplayPrice = product.Price;
        }

        public DisplayProduct(DisplayProductNumber productNumber, Price displayPrice, Product product)
            : this(productNumber, product)
        {
            DisplayPrice = displayPrice;
        }

        private DisplayProductNumber _productNumber;
        public DisplayProductNumber ProductNumber
        {
            get { return _productNumber; }
            set
            {
                _productNumber = value ?? throw new InvalidOperationException(nameof(ProductNumber) + " is required.");
            }
        }

        private Price _displayPrice;
        public Price DisplayPrice
        {
            get { return _displayPrice; }
            set
            {
                _displayPrice = value ?? throw new InvalidOperationException(nameof(DisplayPrice) + " is required.");
            }
        }

        private Product _product;
        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value ?? throw new InvalidOperationException(nameof(Product) + " is reauired.");
            }
        }

        private int _salableStock;
        public int SalableStock
        {
            get { return _salableStock; }
            set
            {
                if (value < 0 || value > 30)
                    throw new InvalidOperationException(nameof(SalableStock) + " must be between 0 and 30.");
                _salableStock = value;
            }
        }

        public bool SoldOut { get { return SalableStock <= 0; } }

        public void Restock(int salableStock)
        {
            SalableStock += salableStock;
        }

        public Product Purchase()
        {
            SalableStock--;
            return new Product(Product.Name, Product.Price); ;
        }
    }
}
