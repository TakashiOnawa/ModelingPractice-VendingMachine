using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class Deposit
    {
        private readonly List<Money> _depositMoney = new List<Money>();
        private DepositAmount _depositAmount = DepositAmount.Empty();

        public void Post(Money money)
        {
            _depositMoney.Add(money);
            _depositAmount = _depositAmount.Add(money);
        }

        public void StorePurchesdAmount(Price price)
        {
            if (!CanPurches(price))
                return;

            _depositAmount = _depositAmount.Minus(price);
        }

        public bool CanPurches(Price price)
        {
            return _depositAmount.Value >= price.Value;
        }

        public IEnumerable<Money> Refund()
        {
            // 今のところ結構てきとうなロジック。
            var refundMoney = new List<Money>();
            var refundAmount = 0;

            foreach (var money in _depositMoney)
            {
                if (refundAmount >= _depositAmount.Value)
                    break;

                refundMoney.Add(money);
                refundAmount += money.Value;
            }

            _depositAmount = DepositAmount.Empty();
            return refundMoney;
        }
    }
}
