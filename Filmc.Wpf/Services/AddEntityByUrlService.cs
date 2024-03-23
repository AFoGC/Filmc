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

        public void CreateFilmByUrl()
        {
            AddEntityByUrlWindow window = new AddEntityByUrlWindow();
            window.ShowDialog();

            if (window.IsUrlWrited)
            {
                EntityResponse response = _client.GetInfoByUrl(window.Url, _languageService.CurrentLanguage);

                if (CheckResponseIsValid(response, DetailedStatus.IsFilm))
                    CreateFilm(response);
            }
        }

        public void CreateBookByUrl()
        {
            AddEntityByUrlWindow window = new AddEntityByUrlWindow();
            window.ShowDialog();

            if (window.IsUrlWrited)
            {
                EntityResponse response = _client.GetInfoByUrl(window.Url, _languageService.CurrentLanguage);

                if (CheckResponseIsValid(response, DetailedStatus.IsBook))
                    CreateBook(response);
            }
        }

        private bool CheckResponseIsValid(EntityResponse response, DetailedStatus expectedCategory)
        {
            if (response.Status == DetailedStatus.IsEmpty)
            {
                //
                return false;
            }

            if (response.Status == DetailedStatus.UrlNotFounded)
            {
                //
                return false;
            }

            if (response.Status != expectedCategory)
            {
                //
                return false;
            }

            return true;
        }

        public void OpenAddEntityWindow(bool isFilmMenu)
        {
            AddEntityByUrlWindow window = new AddEntityByUrlWindow();
            window.ShowDialog();

            if (window.IsUrlWrited)
            {
                EntityResponse response = _client.GetInfoByUrl(window.Url, _languageService.CurrentLanguage);

                if (response.Status == DetailedStatus.IsEmpty)
                {
                    //
                    return;
                }

                if (response.Status == DetailedStatus.UrlNotFounded)
                {
                    //
                    return;
                }

                if (response.Status == DetailedStatus.IsFilm)
                {
                    if (isFilmMenu)
                    {
                        CreateFilm(response);
                    }
                    else
                    {
                        //
                    }

                    return;
                }

                if (response.Status == DetailedStatus.IsBook)
                {
                    if (isFilmMenu)
                    {
                        //
                    }
                    else
                    {
                        CreateBook(response);
                    }

                    return;
                }
            }
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
