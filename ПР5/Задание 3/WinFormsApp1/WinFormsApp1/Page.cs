using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public abstract class Page : IHasReleaseDate, IHasName, IHasDescription
    {
        protected Page(DateTime releaseDate, string name, string description)
        {
            this.releaseDate = releaseDate;
            this.name = name;
            this.description = description;
        }

        public DateTime releaseDate { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public string Name => name;

        public string Description { get => description; set { description = value; } }

        abstract public bool IsValidName(string name);
    }
}
