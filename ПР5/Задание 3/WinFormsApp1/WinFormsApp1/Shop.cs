using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public abstract class Shop : IHasAuthor, IHasName, IHasDescription, IHasReleaseDate
    {
        protected Shop(string authorName, uint authorAge, string authorAbout, string name, string description, DateTime releaseDate)
        {
            AuthorName = authorName;
            AuthorAge = authorAge;
            AuthorAbout = authorAbout;
            Name = name;
            Description = description;
            this.releaseDate = releaseDate;
        }

        public string AuthorName { get; }
        public uint AuthorAge { get; }
        public string AuthorAbout { get; }
        public string Name { get; }
        public string Description { get; set; }
        public DateTime releaseDate { get; set; }

        public abstract bool IsValidName(string name);
        public abstract bool ValidateAuthor();
    }
}
