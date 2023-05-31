using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public interface IForSale
    {
        int price { get; set; }

        int discount { get; set; }

        int getPrice() { return Math.Max(price - discount, 0); }
    }
}
