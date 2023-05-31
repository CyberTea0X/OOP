using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public interface IHasDescription
    {
        string Description { get; set; }

        bool HasDescription() { return Description!= null && Description.Trim().Length > 0; }

        void FormatDescription() { Description = Description.Trim(); }

        void ClearDescription() { Description = ""; }

        void GetDescription() { "Описание: ".Concat(Description); }
    }
}
