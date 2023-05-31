using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public interface IHasAuthor
    {
        string AuthorName { get; }

        uint AuthorAge { get; }

        string AuthorAbout { get; }

        bool ValidateAuthor();
    }
}
