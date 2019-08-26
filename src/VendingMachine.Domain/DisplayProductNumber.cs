using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class DisplayProductNumber : IEquatable<DisplayProductNumber>
    {
        public DisplayProductNumber(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as DisplayProductNumber);
        }

        public bool Equals(DisplayProductNumber other)
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
