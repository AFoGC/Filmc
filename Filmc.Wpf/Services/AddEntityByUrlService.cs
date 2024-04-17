using Filmc.Entities.Entities;
using Filmc.SitesIntegration;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.SettingsServices;
using Filmc.Wpf.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Filmc.Wpf.Services
{
    public class AddEntityByUrlService
    {
        private readonly SitesIntegrationClient _client;
        private readonly ProfilesService _profiles;
        private readonly LanguageService _languageService;

        public AddEntityByUrlService(ProfilesService profiles, LanguageService languageService)
        {
            _client = new SitesIntegrationClient();
            _profiles = profiles;
            _languageService = languageService;
        }

        public async Task CreateFilmByUrl(string url)
        {
            EntityResponse response = await _client.GetInfoByUrl(url, _languageService.CurrentLanguage);

            if (CheckResponseIsValid(response, DetailedStatus.IsFilm))
                CreateFilm(response);
        }

        public async Task CreateBookByUrl(string url)
        {
            EntityResponse response = await _client.GetInfoByUrl(url, _languageService.CurrentLanguage);

            if (CheckResponseIsValid(response, DetailedStatus.IsBook))
                CreateBook(response);
        }

        private bool CheckResponseIsValid(EntityResponse response, DetailedStatus expectedCategory)
        {
            if (response.Status == DetailedStatus.HasError)
            {
                MessageBox.Show("An error occurred while reading this site");
                return false;
            }

            if (response.Status == DetailedStatus.UrlNotSupported)
            {
                MessageBox.Show("This url is not supported");
                return false;
            }

            if (response.Status != expectedCategory)
            {
                MessageBox.Show("Wrong menu (book/films)");
                return false;
            }

            return true;
        }

        public void CreateBook(EntityResponse response)
        {
            Book book = new Book
            {
                Name = response.Name,
                PublicationYear = response.Year
            };

            RepositoriesFacade repositories = _profiles.SelectedProfile.TablesContext;
            book.GenreId = repositories.BookGenres.First().Id;
            repositories.Books.Add(book);
        }

        public void CreateFilm(EntityResponse response)
        {
            Film film = new Film
            {
                Name = response.Name,
                RealiseYear = response.Year
            };

            RepositoriesFacade repositories = _profiles.SelectedProfile.TablesContext;
            film.GenreId = repositories.FilmGenres.First().Id;
            repositories.Films.Add(film);
        }
    }
}
