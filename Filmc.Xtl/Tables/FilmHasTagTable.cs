using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Tables
{
    public class FilmHasTagTable : Table<FilmHasTag>
    {

        public FilmHasTagTable()
        {

        }

        protected override void OnLoaded()
        {
            foreach (var item in this)
                item.FilmId = item.FilmId;

            base.OnLoaded();
        }
    }
}
