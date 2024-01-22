﻿using Filmc.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Repositories
{
    public class FilmInPriorityRepository : BaseRepository<FilmsInPriority>
    {
        public FilmInPriorityRepository(DbSet<FilmsInPriority> dbSet) : base(dbSet)
        {

        }
    }
}
