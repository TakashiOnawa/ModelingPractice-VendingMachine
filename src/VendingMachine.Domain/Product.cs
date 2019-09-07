using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class Product
    {
        public Product(string name, Price price)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
        public Price Price { get; }
    }
}
