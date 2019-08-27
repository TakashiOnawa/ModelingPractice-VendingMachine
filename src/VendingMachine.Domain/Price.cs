using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class Price : IEquatable<Price>
    {
        public Price(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

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
    }
}
