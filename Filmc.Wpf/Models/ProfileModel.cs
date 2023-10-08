using Filmc.Xtl;
using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Models
{
    public class ProfileModel
    {
        public string Name { get; set; }
        public TablesContext TablesContext { get; }

        public ProfileModel()
        {
            Name = String.Empty;
            TablesContext = new TablesContext();

            TablesContext.FilmGenres.Add(new FilmGenre { Name = "Movie", IsSerial = false });
            TablesContext.FilmGenres.Add(new FilmGenre { Name = "Serial", IsSerial = true });

            TablesContext.BookGenres.Add(new BookGenre { Name = "Book" });
        }

        public void SaveTables()
        {

        }

        public void LoadTables()
        {

        }
    }
}
