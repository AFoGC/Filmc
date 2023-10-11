using Filmc.Xtl;
using Filmc.Xtl.Entities;
using Filmc.Xtl.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Models
{
    public class FilmsModel
    {
        private readonly ProfilesModel _profilesModel;


        public FilmsModel(ProfilesModel profilesModel)
        {
            _profilesModel = profilesModel;
            _profilesModel.SelectedProfileChanged += OnSelectedProfileChanged;
        }

        public TablesContext TablesContext => _profilesModel.SelectedProfile.TablesContext;

        public event Action? TablesContextChanged;

        private void OnSelectedProfileChanged(ProfileModel profile)
        {
            TablesContextChanged?.Invoke();
        }

        public void AddCategory()
        {
            TablesContext.FilmCategories.Add();
        }

        public void AddFilm()
        {
            Film film = new Film();
            film.GenreId = TablesContext.FilmGenres.First().Id;
            TablesContext.Films.Add(film);
        }

        public void SaveTables()
        {
            _profilesModel.SelectedProfile.SaveTables();
        }
    }
}
