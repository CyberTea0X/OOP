using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker
{
    public interface IHasPrice
    {
        public int getPrice();
        public void changePrice(int price);
    }
}
