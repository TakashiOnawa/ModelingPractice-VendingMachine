using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class Money : IEquatable<Money>
    {
        public static Money _100 = new Money(100);

        private Money(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Money);
        }

        public bool Equals(Money other)
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
