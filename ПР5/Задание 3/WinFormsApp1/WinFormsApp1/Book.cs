using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public abstract class Book : IForSale, IHasAuthor, IHasDescription, IHasGenre, IHasName, IHasPages, IHasReleaseDate
    {
        protected Book(string name, int price, int discount, string authorName, uint authorAge, string authorAbout, string description, Page[] pages, DateTime releaseDate)
        {
            this.name = name;
            this.price = price;
            this.discount = discount;
            this.authorName = authorName;
            this.authorAge = authorAge;
            this.authorAbout = authorAbout;
            Description = description;
            this.pages = pages;
            this.releaseDate = releaseDate;
        }

        public string name { get; set; }
        public int price { get; set; }
        public int discount { get; set; }
        public string authorName { get; set; }
        public uint authorAge { get; set; }
        public string authorAbout { get; set; }
        public string AuthorName => authorName;

        public uint AuthorAge => authorAge;

        public string AuthorAbout => authorAbout;

        public string Description { get; set; }
        public abstract HashSet<string> Genres { get; }

        public string Name => name;

        public Page[] pages { get; set; }
        public DateTime releaseDate { get; set; }

        public abstract bool IsValidName(string name);

        public abstract bool ValidateAuthor();
    }
}
