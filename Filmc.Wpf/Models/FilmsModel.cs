using Filmc.Wpf.Services;
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
        private readonly ProfilesService _profilesModel;


        public FilmsModel(ProfilesService profilesModel)
        {
            _profilesModel = profilesModel;
            _profilesModel.SelectedProfileChanged += OnSelectedProfileChanged;
        }

        public TablesContext TablesContext => _profilesModel.SelectedProfile.TablesContext;

        public event Action? TablesContextChanged;

        private void OnSelectedProfileChanged(Profile profile)
        {
            TablesContextChanged?.Invoke();
        }

        public void AddCategory()
        {
            TablesContext.FilmCategories.Add();
        }

        public void RemoveCategory(FilmCategory category)
        {
            TablesContext.FilmCategories.Remove(category);
        }

        public void AddFilm()
        {
            Film film = new Film();
            film.GenreId = TablesContext.FilmGenres.First().Id;
            TablesContext.Films.Add(film);
        }

        public void AddFilm(int categoryId)
        {
            Film film = new Film();
            film.GenreId = TablesContext.FilmGenres.First().Id;
            TablesContext.Films.Add(film);

            TablesContext.FilmCategories
                .First(x => x.Id == categoryId)
                .AddFilmInOrder(film);
        }

        public void SaveTables()
        {
            _profilesModel.SelectedProfile.SaveTables();
        }
    }
}
