using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Tables
{
    public class BookCategoriesTable : Table<BookCategory>
    {
        private int _markSystem;

        public BookCategoriesTable()
        {
            _markSystem = 6;
        }

        public int MarkSystem
        {
            get => _markSystem;
            set { _markSystem = value; OnPropertyChanged(); }
        }
    }
}
