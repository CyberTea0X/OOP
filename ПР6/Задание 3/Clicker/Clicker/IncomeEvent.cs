using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Clicker
{
    public class IncomeEvent: EventArgs
    {
        public BigInteger income;

        public IncomeEvent(BigInteger income): base()
        {
            this.income = income;
        }
    }
}
