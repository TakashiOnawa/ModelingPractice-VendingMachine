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
        }

        public DisplayProductNumber ProductNumber { get; private set; }
        public Product Product { get; private set; }
    }
}
