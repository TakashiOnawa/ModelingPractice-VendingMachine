using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class DisplayProduct
    {
        public DisplayProduct(DisplayProductNumber productNumber, Product product)
        {
            ProductNumber = productNumber ?? throw new InvalidOperationException(nameof(ProductNumber) + " is required."); ;
            Product = product;
            DisplayPrice = product.Price;
            SalableStock = ProductStockQuantity.EmptyQuantity();
        }

        public DisplayProduct(DisplayProductNumber productNumber, Price displayPrice, Product product)
            : this(productNumber, product)
        {
            DisplayPrice = displayPrice;
        }

        public DisplayProductNumber ProductNumber { get; }

        private Price _displayPrice;
        public Price DisplayPrice
        {
            get { return _displayPrice; }
            private set
            {
                _displayPrice = value ?? throw new InvalidOperationException(nameof(DisplayPrice) + " is required.");
            }
        }

        private Product _product;
        public Product Product
        {
            get { return _product; }
            private set
            {
                _product = value ?? throw new InvalidOperationException(nameof(Product) + " is reauired.");
            }
        }

        private ProductStockQuantity _salableStock;
        public ProductStockQuantity SalableStock
        {
            get { return _salableStock; }
            private set
            {
                _salableStock = value ?? throw new InvalidOperationException(nameof(SalableStock) + " is required.");
            }
        }

        public bool SoldOut { get { return SalableStock.Empty; } }

        public void Restock(ProductStockQuantity salableStock)
        {
            SalableStock = SalableStock.Add(salableStock);
        }

        public Product Purchase()
        {
            SalableStock = SalableStock.Minus(new ProductStockQuantity(1));
            return new Product(Product.Name, Product.Price); ;
        }
    }
}
