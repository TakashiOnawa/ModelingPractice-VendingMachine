using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class DisplayProductNumber
    {
        public DisplayProductNumber(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }
    }
}
