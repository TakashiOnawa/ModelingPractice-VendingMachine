using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine.Domain
{
    public class Deposit
    {
        private readonly List<Money> _depositMoneyList = new List<Money>();
        private Amount _insertedAmount = Amount.EmptyAmount();

        public void Restock(IEnumerable<Money> change)
        {
            _depositMoneyList.AddRange(change);
        }

        public void Post(Money money)
        {
            _depositMoneyList.Add(money);
            _insertedAmount = _insertedAmount.Add(money);
        }

        public void StorePurchesdAmount(Price price)
        {
            if (!CanPurches(price))
                throw new InvalidOperationException("Can't purches.");

            _insertedAmount = _insertedAmount.Minus(price);
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

            foreach (var groupingMoney in _depositMoneyList.GroupBy(_ => _.Value).OrderBy(_ => _.Key))
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
