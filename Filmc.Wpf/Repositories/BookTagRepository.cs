using Filmc.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Repositories
{
    public class BookTagRepository : BaseRepository<BookTag>
    {
        public BookTagRepository(DbSet<BookTag> dbSet) : base(dbSet)
        {

        }
    }
}
