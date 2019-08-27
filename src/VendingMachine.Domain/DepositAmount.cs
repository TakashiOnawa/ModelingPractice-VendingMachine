using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class DepositAmount
    {
        public DepositAmount(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public static DepositAmount Empty()
        {
            return new DepositAmount(0);
        }

        public DepositAmount Add(Money money)
        {
            return new DepositAmount(Value + money.Value);
        }

        public DepositAmount Minus(Price price)
        {
            return new DepositAmount(Value - price.Value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DepositAmount);
        }

        public bool Equals(DepositAmount other)
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
