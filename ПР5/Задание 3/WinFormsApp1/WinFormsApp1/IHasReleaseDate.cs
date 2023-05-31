using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public interface IHasReleaseDate
    {
        DateTime releaseDate { get; set; }

        void ReleaseBook() { releaseDate = DateTime.Now.ToUniversalTime(); }

        void SetReleaseDate(DateTime releaseDate) { releaseDate = releaseDate.ToUniversalTime(); }

        DateTime ReleasedAt() { return releaseDate; }

        bool isOlderReleased(DateTime someDate) { return releaseDate > someDate; }

        bool isNewerReleased(DateTime someDate) { return releaseDate < someDate; }
    }
}
