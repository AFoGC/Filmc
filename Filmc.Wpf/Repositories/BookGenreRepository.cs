using Filmc.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Repositories
{
    public class BookGenreRepository : BaseRepository<BookGenre>
    {
        public BookGenreRepository(DbSet<BookGenre> dbSet) : base(dbSet)
        {

        }
    }
}
