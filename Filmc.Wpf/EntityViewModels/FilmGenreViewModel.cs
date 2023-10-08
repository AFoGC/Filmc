using Filmc.Wpf.ViewModels;
using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.EntityViewModels
{
    public class FilmGenreViewModel : BaseViewModel
    {
        public FilmGenre Model { get; }

        public FilmGenreViewModel(FilmGenre model)
        {
            Model = model;
        }
    }
}
