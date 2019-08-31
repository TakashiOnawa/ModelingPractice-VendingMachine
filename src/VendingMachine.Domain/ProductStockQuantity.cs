using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class ProductStockQuantity : IEquatable<ProductStockQuantity>
    {
        public ProductStockQuantity(int value)
        {
            if (value < 0 || value > 30) throw new ArgumentException("Argument must be between 0 and 30.");
            Value = value;
        }

        public int Value { get; private set; }
        public bool Empty { get { return Value == 0; } }

        public static ProductStockQuantity EmptyQuantity()
        {
            return new ProductStockQuantity(0);
        }

        public ProductStockQuantity Add(ProductStockQuantity stockQuantity)
        {
            return new ProductStockQuantity(Value + stockQuantity.Value);
        }

        public ProductStockQuantity Minus(ProductStockQuantity stockQuantity)
        {
            return new ProductStockQuantity(Value - stockQuantity.Value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProductStockQuantity);
        }

        public bool Equals(ProductStockQuantity other)
        {
            return other != null &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
