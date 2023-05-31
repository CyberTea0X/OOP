using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public interface IHasGenre
    {
        HashSet<string> Genres { get; }

        bool ContainsGenre(string genre) { return Genres.Contains(genre); }

        string[] GetGenres() { return Genres.ToArray(); }

        void AddGenre(string genre) { Genres.Add(genre); }

        void RemoveGenre(string genre) { Genres.Remove(genre); }

    }
}
