using Filmc.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Entities.Repositories
{
    public class BookReadProgressRepository : BaseRepository<BookReadProgress>
    {
        public BookReadProgressRepository(DbSet<BookReadProgress> dbSet) : base(dbSet)
        {

        }

        public override void Add(BookReadProgress item)
        {
            if (item.Id == 0)
                item.Id = GetNewId(x => x.Id);

            base.Add(item);
        }
    }
}
