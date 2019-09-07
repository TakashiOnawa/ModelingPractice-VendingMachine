using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class Price : IEquatable<Price>, IComparable<Price>
    {
        public Price(int value)
        {
            if (value < 0) throw new ArgumentException(nameof(value) + " is negative value.");
            Value = value;
        }

        public int Value { get; private set; }

        public static Price FreePrice()
        {
            return new Price(0);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Price);
        }

        public bool Equals(Price other)
        {
            return other != null &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public int CompareTo(Price price)
        {
            return Value.CompareTo(price.Value);
        }
    }
}
