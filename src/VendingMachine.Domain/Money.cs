using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class Money
    {
        public static Money _100 = new Money(100);

        private Money(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }
    }
}
