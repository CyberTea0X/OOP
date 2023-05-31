using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public interface IHasPages
    {
        Page[] pages { get; set; }

        int pagesCount() { return pages.Length; }

        void addPage(Page newPage) { pages.Append(newPage); }
        void removePage(int pageNumber) { pages = pages.Where((_, idx) => idx != pageNumber).ToArray(); }
    }
}
