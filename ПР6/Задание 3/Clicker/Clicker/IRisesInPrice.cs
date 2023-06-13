using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker
{
    public interface IRisesInPrice: IHasPrice
    {
        public double getPriceGrowth();
        public void increasePrice()
        {
            changePrice((int)Math.Round((double)getPrice() * (double)getPriceGrowth()));
        }

        public int PreviousPrice();
    }
}
