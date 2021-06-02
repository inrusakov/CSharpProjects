using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseTask2
{
    class Sale :IEntity
    {
        public long Id { get; }

        public long BuyerId { get; }

        public long ShopId { get; }
        
        public long GoodId { get; }

        public long Amount { get; }

        public long Price { get; }

        public Sale(long id, long buyerId, long shopId,
            long goodId, long amount, long price)
        {
            Id = id;
            BuyerId = buyerId;
            ShopId = shopId;
            GoodId = goodId;
            Amount = amount;
            Price = price;
        }

        public override string ToString()
        {
            return $"id: {Id}," +
                $" buyerID: {BuyerId}," +
                $" ShopID: {ShopId}," +
                $" GoodID: {GoodId}," +
                $" Amount: {Amount}," +
                $" Price: {Price}";
        }
    }
}
