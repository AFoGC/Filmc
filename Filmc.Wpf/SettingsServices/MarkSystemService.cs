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

        public event Action? FilmsMarkSystemChanged;
        public event Action? BooksMarkSystemChanged;

        public int FilmMarkSystem
        {
            get => _profilesService.SelectedProfile.Settings.FilmsMarkSystem;
            set
            {
                _profilesService.SelectedProfile.Settings.FilmsMarkSystem = value;
                _profilesService.SelectedProfile.TablesContext.SaveChanges();
                FilmsMarkSystemChanged?.Invoke();
            }
        }

        public int BookMarkSystem
        {
            get => _profilesService.SelectedProfile.Settings.BooksMarkSystem;
            set
            {
                _profilesService.SelectedProfile.Settings.BooksMarkSystem = value;
                _profilesService.SelectedProfile.TablesContext.SaveChanges();
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
