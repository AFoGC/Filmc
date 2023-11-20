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
            set
            { 
                _markSystem = value; 

                foreach (var item in this)
                    item.Mark.MarkSystem = _markSystem;

                OnPropertyChanged(); 
            }
        }

        public override BookCategory Add(BookCategory item)
        {
            BookCategory category = base.Add(item);

            category.Mark.MarkSystem = MarkSystem;

            return category;
        }

        protected override void OnLoaded()
        {
            foreach (var item in this)
                item.Mark.MarkSystem = this.MarkSystem;

            base.OnLoaded();
        }
    }
}
