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
            Price = product.Price;
        }

        public DisplayProduct(DisplayProductNumber productNumber, Price price, Product product)
        {
            ProductNumber = productNumber ?? throw new ArgumentNullException(nameof(productNumber));
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }

        public DisplayProductNumber ProductNumber { get; private set; }
        public Price Price { get; private set; }
        public SalesStatus SalesStatus { get; private set; } = SalesStatus.Unsalable;
        public Product Product { get; private set; }

        public bool CanPurchase(Deposit deposit)
        {
            return deposit.CanPurches(Price);
        }

        public Product Purchase()
        {
            return new Product(Product.Name, Product.Price);
        }
    }
}
