using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker
{
    public abstract class Product : IHasPrice, IRisesInPrice, IBuyable
    {
        public event EventHandler<BuyEventArgs>? BoughtEvent;
        private int price;
        private double priceGrowth;
        private int previousPrice;
        private int soldCount = 0;

        public Product(int price, double priceGrowth)
        {
            this.price = price;
            this.priceGrowth = priceGrowth;
            previousPrice = price;
        }

        public void changePrice(int price)
        {
            this.price = price;
        }

        public int getPrice() => price;

        public double getPriceGrowth() => priceGrowth;

        public virtual void Buy()
        {
            previousPrice = price;
            BoughtEvent?.Invoke(this, new BuyEventArgs(price));
            soldCount++;
            ((IRisesInPrice)this).increasePrice();
        }
        public virtual int getBougthCount() => soldCount;

        public int PreviousPrice() => previousPrice;
    }
}
