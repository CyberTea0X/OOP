using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clicker
{
    public interface IBuyable : IHasPrice
    {
        public event EventHandler<BuyEventArgs> BoughtEvent;
        public void Buy();

        public int getBougthCount();
    }
}
