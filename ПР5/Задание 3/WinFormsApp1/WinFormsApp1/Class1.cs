using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public abstract class Page : IHasReleaseDate, IHasName, IHasDescription
    {
        public DateTime releaseDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Name => throw new NotImplementedException();

        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsValidName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
