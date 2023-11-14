using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Tables
{
    public class FilmCategoriesTable : Table<FilmCategory>
    {
        private int _markSystem;

        public FilmCategoriesTable()
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
    }
}
