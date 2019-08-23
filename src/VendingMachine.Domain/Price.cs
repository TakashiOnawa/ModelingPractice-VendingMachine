using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class Price
    {
        public Price(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }
    }
}
