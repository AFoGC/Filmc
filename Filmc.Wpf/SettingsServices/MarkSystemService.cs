using Filmc.Wpf.Repositories;
using Filmc.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.SettingsServices
{
    public class MarkSystemService
    {
        private readonly ProfilesService _profilesService;

        public MarkSystemService(ProfilesService profilesService)
        {
            _profilesService = profilesService;
            _profilesService.SelectedProfileChanged += OnSelectedProfileChanged;
        }

        public Action? FilmsMarkSystemChanged;
        public Action? BooksMarkSystemChanged;

        public int FilmMarkSystem
        {
            get => _profilesService.SelectedProfile.TablesContext.Films.MarkSystem;
            set
            {
                RepositoriesFacade tablesContext = _profilesService.SelectedProfile.TablesContext;

                tablesContext.Films.MarkSystem = value;
                tablesContext.FilmCategories.MarkSystem = value;

                FilmsMarkSystemChanged?.Invoke();
            }
        }

        public int BookMarkSystem
        {
            get => _profilesService.SelectedProfile.TablesContext.Books.MarkSystem;
            set
            {
                RepositoriesFacade tablesContext = _profilesService.SelectedProfile.TablesContext;

                tablesContext.Books.MarkSystem = value;
                tablesContext.BookCategories.MarkSystem = value;

                BooksMarkSystemChanged?.Invoke();
            }
        }

        private void OnSelectedProfileChanged(Profile obj)
        {
            FilmsMarkSystemChanged?.Invoke();
            BooksMarkSystemChanged?.Invoke();
        }
    }
}
