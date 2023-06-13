using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker
{
    public class BuyEventArgs : EventArgs
    {
        public int price;

        public BuyEventArgs(int price)
        {
            this.price = price;
        }
    }
}
