using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class DisplayProduct
    {
        public DisplayProduct(DisplayProductNumber productNumber, Product product)
        {
            ProductNumber = productNumber ?? throw new ArgumentNullException(nameof(productNumber));
            Product = product ?? throw new ArgumentNullException(nameof(product));
            DisplayPrice = product.Price;
        }

        public DisplayProduct(DisplayProductNumber productNumber, Price displayPrice, Product product)
            : this(productNumber, product)
        {
            DisplayPrice = displayPrice ?? throw new ArgumentNullException(nameof(displayPrice));
        }

        public DisplayProductNumber ProductNumber { get; private set; }
        public Price DisplayPrice { get; private set; }
        public Product Product { get; private set; }
        public int SalableStock { get; private set; }
        public bool SoldOut { get { return SalableStock <= 0; } }

        public void Restock(int salableStock)
        {
            SalableStock += salableStock;
        }

        public Product Purchase()
        {
            if (SoldOut)
                return null;

            var product = new Product(Product.Name, Product.Price);
            SalableStock--;
            return product;
        }
    }
}
