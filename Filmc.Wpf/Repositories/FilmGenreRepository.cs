﻿using Filmc.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Repositories
{
    public class FilmGenreRepository : BaseRepository<FilmGenre>
    {
        public FilmGenreRepository(DbSet<FilmGenre> dbSet) : base(dbSet)
        {

        }

        public override void Add(FilmGenre item)
        {
            if (item.Id == 0)
                item.Id = GetNewId(x => x.Id);

            base.Add(item);
        }
    }
}
