using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.EntityViewModels
{
    public class FilmCategoryViewModel : BaseEntityViewModel
    {
        public FilmCategory Model { get; }

        public FilmCategoryViewModel(FilmCategory model)
        {
            Model = model;
        }

        public override bool Search(string search)
        {
            throw new NotImplementedException();
        }
    }
}
