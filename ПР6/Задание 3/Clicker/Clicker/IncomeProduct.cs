using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker
{
    public class IncomeProduct : Product, IIncreasesIncome
    {
        public event EventHandler<IncreaseIncomeEventArgs>? IncreaseIncome;
        private int incomeAdds;
        public IncomeProduct(int price, double priceGrowth, int incomeAdds) : base(price, priceGrowth)
        {
            this.incomeAdds = incomeAdds;

        }

        public void OnIncreaseIncome() => IncreaseIncome?.Invoke(this, new IncreaseIncomeEventArgs(incomeAdds));

        public override void Buy()
        {
            base.Buy();
            OnIncreaseIncome();
        }
    }
}
