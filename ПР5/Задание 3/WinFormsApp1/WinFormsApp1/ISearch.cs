using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public interface ISearch <T>
    {
        T[] SearchAll();

        T Find(Func<T, bool> predicate);
    }
}
