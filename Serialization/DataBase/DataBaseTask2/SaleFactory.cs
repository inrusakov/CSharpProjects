using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseTask2
{
    class SaleFactory :IEntityFactory<Sale>
    {
        private static long _id = 0;

        private long _buyerId;

        private long _shopId;

        private long _goodId;

        private long _amount;

        private long _price;

        public SaleFactory(long buyerId, long shopId,
            long goodId, long amount, long price)
        {
            _buyerId = buyerId;
            _shopId = shopId;
            _goodId = goodId;
            _amount = amount;
            _price = price;
        }

        public SaleFactory(bool renew)
        {
            _id = 0;
        }

        public Sale Instance => new Sale(_id++, _buyerId, _shopId, _goodId, _amount, _price);
    }
}
