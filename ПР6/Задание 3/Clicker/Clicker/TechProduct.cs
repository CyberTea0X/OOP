using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker
{
    public class TechProduct : Product, ITech
    {
        public event EventHandler<TechEventArgs>? TechEvent;
        public TechProduct(int price, int priceGrowth) : base(price, priceGrowth)
        {
        }

        public override void Buy()
        {
            base.Buy();
            TechEvent?.Invoke(this, new TechEventArgs());
        }

        public void TechUpdate()
        {
            TechEvent?.Invoke(this, new TechEventArgs());
        }
    }
}
