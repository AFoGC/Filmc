using Filmc.Entities.Context;
using Filmc.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Repositories
{
    public class BookCategoryRepository : BaseRepository<BookCategory>
    {
        private int _markSystem;

        public BookCategoryRepository(DbSet<BookCategory> dbSet) : base(dbSet)
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
            }
        }

        public override void Add(BookCategory item)
        {
            base.Add(item);
            item.Mark.MarkSystem = _markSystem;
        }
    }
}
