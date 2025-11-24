using Filmc.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Entities.Repositories
{
    public class BookTagCategoryRepository : BaseRepository<BookTagCategory>
    {
        public BookTagCategoryRepository(DbSet<BookTagCategory> dbSet) : base(dbSet)
        {

        }

        public override void Add(BookTagCategory item)
        {
            if (item.Id == 0)
                item.Id = GetNewId(x => x.Id);

            base.Add(item);
        }
    }
}
