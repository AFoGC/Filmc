using Filmc.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Entities.Repositories
{
    public class BookGenreRepository : BaseRepository<BookGenre>
    {
        public BookGenreRepository(DbSet<BookGenre> dbSet) : base(dbSet)
        {

        }

        public override void Add(BookGenre item)
        {
            if (item.Id == 0)
                item.Id = GetNewId(x => x.Id);

            base.Add(item);
        }
    }
}
