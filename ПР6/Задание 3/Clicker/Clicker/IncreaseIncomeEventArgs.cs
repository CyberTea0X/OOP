using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker
{
    public class IncreaseIncomeEventArgs : EventArgs
    {
        public int IncomeAdds;

        public IncreaseIncomeEventArgs(int incomeAdds)
        {
            IncomeAdds = incomeAdds;
        }
    }
}
