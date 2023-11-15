using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Tables
{
    public class BooksTable : Table<Book>
    {
        private int _markSystem;

        public BooksTable()
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

        public override Book Add(Book item)
        {
            Book book = base.Add(item);

            book.Mark.MarkSystem = MarkSystem;

            return book;
        }
    }
}
