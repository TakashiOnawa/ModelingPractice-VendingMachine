using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine.Domain
{
    public class Deposit
    {
        private readonly MoneyStocks _moneyStocks = new MoneyStocks();
        private Amount _insertedAmount = Amount.EmptyAmount();

        public void SetMoneyStock(MoneyStock moneyStock)
        {
            _moneyStocks.SetMoneyStock(moneyStock);
        }

        public void RestockChange(Money money)
        {
            var moneyStock = _moneyStocks.FindWithValidation(money);
            moneyStock.Add(money);
        }

        public void Post(Money money)
        {
            var moneyStock = _moneyStocks.FindWithValidation(money);
            moneyStock.Add(money);
            _insertedAmount = _insertedAmount.Add(money);
        }

        public void StorePurchesdAmount(Price price)
        {
            if (!CanPurches(price))
                throw new InvalidOperationException("Can't purches.");

            _insertedAmount = _insertedAmount.Minus(price);
        }

        public bool CanPost(Money money)
        {
            var moneyStock = _moneyStocks.Find(money);
            if (moneyStock == null) return false;
            if (moneyStock.Full) return false;
            return true;
        }

        public bool CanPurches(Price price)
        {
            return _insertedAmount.Value >= price.Value && CanRefund(price);
        }

        public bool CanRefund(Price price)
        {
            if (_insertedAmount.Value < price.Value)
                return false;

            var remainingAmount = Refund(price, out var refundMoneyList);

            return remainingAmount.Empty;
        }

        public IEnumerable<Money> Refund()
        {
            var remainingAmount = Refund(Price.FreePrice(), out var refundMoneyList);

            if (!remainingAmount.Empty)
                throw new InvalidOperationException("Can't return change.");

            return refundMoneyList;
        }

        private Amount Refund(Price exceptPrice, out List<Money> refundMoneyList)
        {
            refundMoneyList = new List<Money>();

            var remainingAmount = _insertedAmount.Minus(exceptPrice);

            foreach (var groupingMoney in _moneyStocks.AllMoney().GroupBy(_ => _.Value).OrderBy(_ => _.Key))
            {
                var currentMoneyList = groupingMoney.ToList();
                var currentMoneyAmount = groupingMoney.Key;

                while (remainingAmount.Value >= currentMoneyAmount && currentMoneyList.Count > 0)
                {
                    var money = currentMoneyList.Last();
                    remainingAmount = remainingAmount.Minus(money);
                    currentMoneyList.Remove(money);
                    refundMoneyList.Add(money);
                }
            }

            return remainingAmount;
        }
    }
}
