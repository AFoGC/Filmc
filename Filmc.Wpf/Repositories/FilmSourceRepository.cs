using Filmc.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Repositories
{
    public class FilmSourceRepository : BaseRepository<FilmSource>
    {
        public FilmSourceRepository(DbSet<FilmSource> dbSet) : base(dbSet)
        {

        }
    }
}
