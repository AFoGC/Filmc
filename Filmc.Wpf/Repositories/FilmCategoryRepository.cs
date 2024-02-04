using Filmc.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Repositories
{
    public class FilmCategoryRepository : BaseRepository<FilmCategory>
    {
        private int _markSystem;

        public FilmCategoryRepository(DbSet<FilmCategory> dbSet) : base(dbSet)
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

        public override void Add(FilmCategory item)
        {
            if (item.Id == 0)
                item.Id = GetNewId(x => x.Id);

            base.Add(item);
            item.Mark.MarkSystem = _markSystem;
        }

        public void Add()
        {
            FilmCategory item = new FilmCategory();
            item.Id = GetNewId(x => x.Id);

            Add(item);
        }
    }
}
