using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine.Domain
{
    public class MoneyStocks
    {
        private readonly ConcurrentDictionary<Money, MoneyStock> _moneyStocks = new ConcurrentDictionary<Money, MoneyStock>();

        public IEnumerable<Money> UsableMoney { get { return _moneyStocks.Keys; } }
        public IEnumerable<Money> AllMoney { get { return _moneyStocks.Values.SelectMany(_ => _.StockedMoney).ToArray(); } }

        public void SetMoneyStock(MoneyStock moneyStock)
        {
            _moneyStocks.AddOrUpdate(moneyStock.MoneyType, moneyStock, (key, value) => moneyStock);
        }

        public MoneyStock Find(Money moneyType)
        {
            _moneyStocks.TryGetValue(moneyType, out var moneyStock);
            return moneyStock;
        }

        public MoneyStock FindWithValidation(Money moneyType)
        {
            var moneyStock = Find(moneyType);
            if (moneyStock == null) throw new InvalidOperationException("MoneyStock is not exists.");
            return moneyStock;
        }
    }
}
