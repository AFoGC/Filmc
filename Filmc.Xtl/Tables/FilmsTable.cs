using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Tables
{
    public class FilmsTable : Table<Film>
    {
        private int _markSystem;

        public FilmsTable()
        {
            _markSystem = 6;
        }

        public int MarkSystem
        {
            get => _markSystem;
            set
            { 
                _markSystem = value; 

                foreach (var item in this)
                    item.Mark.MarkSystem = _markSystem;

                OnPropertyChanged(); 
            }
        }

        public override Film Add(Film item)
        {
            Film film = base.Add(item);

            film.Mark.MarkSystem = MarkSystem;

            return film;
        }
    }
}
