using Filmc.Entities.Entities;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.Services;
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

        public RepositoriesFacade TablesContext => _profilesModel.SelectedProfile.TablesContext;

        public event Action? TablesContextChanged;

        private void OnSelectedProfileChanged(Profile profile)
        {
            TablesContextChanged?.Invoke();
        }

        public void AddCategory()
        {
            TablesContext.FilmCategories.Add();
            TablesContext.SaveChanges();
        }

        public void RemoveCategory(FilmCategory category)
        {
            TablesContext.FilmCategories.Remove(category);
            TablesContext.SaveChanges();
        }

        public void AddFilm()
        {
            Film film = new Film();
            film.GenreId = TablesContext.FilmGenres.First().Id;
            TablesContext.Films.Add(film);
            TablesContext.SaveChanges();
        }

        public void AddFilm(FilmCategory category)
        {
            Film film = new Film();
            film.GenreId = TablesContext.FilmGenres.First().Id;

            if (category.HideName != String.Empty)
            {
                film.Name = category.HideName;
            }
            else
            {
                film.Name = category.Name;
            }

            TablesContext.Films.Add(film);

            TablesContext.FilmCategories
                .First(x => x.Id == category.Id)
                .AddFilmInOrder(film);

            TablesContext.SaveChanges();
        }

        public void DeleteFilm(Film film)
        {
            if (film.CategoryId != 0)
                film.CategoryId = 0;

            if (film.GenreId != 0)
                film.GenreId = 0;

            if (film.Priority != null)
                TablesContext.FilmInPriorities.Remove(film.Priority);

            TablesContext.Films.Remove(film);
            TablesContext.SaveChanges();
        }

        public void AddFilmToCategory(FilmCategory category, Film film)
        {
            category.AddFilmInOrder(film);
            TablesContext.SaveChanges();
        }

        public void RemoveFilmFromCategory(FilmCategory category, Film film)
        {
            category.RemoveFilmInOrder(film);
            TablesContext.SaveChanges();
        }

        public void AddFilmToPriority(Film film)
        {
            if (TablesContext.FilmInPriorities.All(x => x.Id != film.Id))
            {
                FilmsInPriority priority = new FilmsInPriority
                { 
                    Id = film.Id, 
                    CreationTime = DateTime.Now 
                };

                TablesContext.FilmInPriorities.Add(priority);
            }

            TablesContext.SaveChanges();
        }

        public void RemoveFilmFromPriority(Film film)
        {
            if (film.Priority != null)
                TablesContext.FilmInPriorities.Remove(film.Priority);

            TablesContext.SaveChanges();
        }

        public void SaveTables()
        {
            _profilesModel.SelectedProfile.SaveTables();
        }
    }
}
