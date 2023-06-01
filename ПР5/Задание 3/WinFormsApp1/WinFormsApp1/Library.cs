using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public abstract class Library : ISearch<Book>
    {
        public abstract Book Find(Func<Book, bool> predicate);
        public abstract Book[] SearchAll();
    }
}
