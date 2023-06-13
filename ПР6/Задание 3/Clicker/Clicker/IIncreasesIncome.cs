using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker
{
    public interface IIncreasesIncome
    {
        public event EventHandler<IncreaseIncomeEventArgs> IncreaseIncome;
        void OnIncreaseIncome();
    }
}
