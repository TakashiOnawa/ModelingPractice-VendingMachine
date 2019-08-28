using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class Amount
    {
        public Amount(int value)
        {
            if (value < 0) throw new ArgumentException("Argument is negative value.");
            Value = value;
        }

        public int Value { get; private set; }
        public bool Empty { get { return Value == 0; } }

        public static Amount EmptyAmount()
        {
            return new Amount(0);
        }

        public Amount Add(Money money)
        {
            return new Amount(Value + money.Value);
        }

        public Amount Minus(Price price)
        {
            return new Amount(Value - price.Value);
        }

        public Amount Minus(Money money)
        {
            return new Amount(Value - money.Value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Amount);
        }

        public bool Equals(Amount other)
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
