using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Domain
{
    public class Deposit
    {
        private readonly List<Money> _depositMoney = new List<Money>();
        private long _depositAmount;

        public void Post(Money money)
        {
            _depositMoney.Add(money);
            _depositAmount += money.Value;
        }

        public IEnumerable<Money> Refund()
        {
            var refundMoney = new List<Money>();
            var refundAmount = 0;

            foreach (var money in _depositMoney)
            {
                if (refundAmount >= _depositAmount)
                    break;

                refundMoney.Add(money);
                refundAmount += money.Value;
            }

            _depositAmount = 0;
            return refundMoney;
        }

        public void StorePurchesdAmount(Price price)
        {
            _depositAmount -= price.Value;
        }

        public bool CanPurches(DisplayProduct displayProduct)
        {
            return _depositAmount >= displayProduct.Product.Price.Value;
        }
    }
}
