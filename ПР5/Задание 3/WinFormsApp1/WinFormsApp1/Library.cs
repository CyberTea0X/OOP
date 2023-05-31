using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public abstract class Library : ISearch<Page>
    {
        public abstract Page Find(Func<Page, bool> predicate);
        public abstract Page[] SearchAll();
    }
}
