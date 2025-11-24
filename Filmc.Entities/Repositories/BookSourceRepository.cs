using Filmc.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Entities.Repositories
{
    public class BookSourceRepository : BaseRepository<BookSource>
    {
        public BookSourceRepository(DbSet<BookSource> dbSet) : base(dbSet)
        {

        }

        public override void Add(BookSource item)
        {
            if (item.Id == 0)
                item.Id = GetNewId(x => x.Id);

            base.Add(item);
        }
    }
}
