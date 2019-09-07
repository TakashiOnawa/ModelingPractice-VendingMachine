using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class MoneyStock
    {
        private readonly List<Money> _stockedMoney = new List<Money>();
        private readonly int _maxStockCount;

        public MoneyStock(Money moneyType, int maxStockCount)
        {
            MoneyType = moneyType ?? throw new ArgumentNullException(nameof(moneyType));

            if (maxStockCount < 0) throw new ArgumentException(nameof(maxStockCount) + " is zero or negative value.");
            _maxStockCount = maxStockCount;
        }

        public Money MoneyType {get;}
        public IEnumerable<Money> StockedMoney { get { return _stockedMoney; } }
        public bool Full { get { return _stockedMoney.Count >= _maxStockCount; } }

        public void Add(Money money)
        {
            if (MoneyType != money) throw new InvalidOperationException("Money type is different.");
            if (Full) throw new InvalidOperationException("Stock is full.");

            _stockedMoney.Add(money);
        }
    }
}
