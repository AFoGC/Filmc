using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Services;
using Filmc.Wpf.SettingsServices;
using Filmc.Xtl.Entities;
using Filmc.Xtl.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.ViewModels
{
    public class SettingsTablesViewModel : BaseViewModel
    {
        private readonly ProfilesService _profilesService;

        private FilmGenresTable? _filmGenres;
        private BookGenresTable? _bookGenres;

        public SettingsTablesViewModel(SettingsService settingsService)
        {
            _profilesService = settingsService.ProfilesService;

            ProfileVMs = new ObservableCollection<ProfileViewModel>();
            FilmGenresVMs = new ObservableCollection<FilmGenreViewModel>();
            BookGenresVMs = new ObservableCollection<BookGenreViewModel>();

            foreach (var profile in _profilesService.Profiles)
                ProfileVMs.Add(new ProfileViewModel(profile));

            OnProfileChanged(_profilesService.SelectedProfile);
            _profilesService.SelectedProfileChanged += OnProfileChanged;
            _profilesService.ProfileAdded += OnProfileAdded;
            _profilesService.ProfileRemoved += ProfileRemoved;
        }

        public ObservableCollection<ProfileViewModel> ProfileVMs { get; }
        public ObservableCollection<FilmGenreViewModel> FilmGenresVMs { get; }
        public ObservableCollection<BookGenreViewModel> BookGenresVMs { get; }

        public FilmGenresTable? FilmGenres
        {
            get => _filmGenres;
            set { _filmGenres = value; OnPropertyChanged(); }
        }
        public BookGenresTable? BookGenres
        {
            get => _bookGenres;
            set { _bookGenres = value; OnPropertyChanged(); }
        }

        private void OnProfileAdded(Profile newProfile)
        {
            ProfileVMs.Add(new ProfileViewModel(newProfile));
        }

        private void ProfileRemoved(Profile oldProfile)
        {
            ProfileViewModel? profileVM = ProfileVMs.FirstOrDefault(x => x.Profile == oldProfile);

            if (profileVM != null)
                ProfileVMs.Remove(profileVM);
        }

        private void OnProfileChanged(Profile newProfile)
        {
            foreach (var profileVm in ProfileVMs)
            {
                profileVm.IsSelected = false;

                if (profileVm.Profile == newProfile)
                    profileVm.IsSelected = true;
            }

            if (FilmGenres != null)
                FilmGenres.CollectionChanged -= OnFilmGenresCollectionChanged;

            if (BookGenres != null)
                BookGenres.CollectionChanged -= OnBookGenresCollectionChanged;

            FilmGenres = newProfile.TablesContext.FilmGenres;
            BookGenres = newProfile.TablesContext.BookGenres;

            FilmGenresVMs.Clear();
            foreach (var item in FilmGenres)
                FilmGenresVMs.Add(new FilmGenreViewModel(item));

            BookGenresVMs.Clear();
            foreach (var item in BookGenres)
                BookGenresVMs.Add(new BookGenreViewModel(item));

            FilmGenres.CollectionChanged += OnFilmGenresCollectionChanged;
            BookGenres.CollectionChanged += OnBookGenresCollectionChanged;
        }

        private void OnFilmGenresCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                FilmGenre entity = (FilmGenre)e.NewItems[0]!;
                FilmGenresVMs.Insert(e.NewStartingIndex, new FilmGenreViewModel(entity));
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                int i = e.OldStartingIndex;
                FilmGenresVMs.RemoveAt(i);
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                FilmGenresVMs.Clear();
            }
        }

        private void OnBookGenresCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                BookGenre entity = (BookGenre)e.NewItems[0]!;
                BookGenresVMs.Insert(e.NewStartingIndex, new BookGenreViewModel(entity)); ;
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                int i = e.OldStartingIndex;
                BookGenresVMs.RemoveAt(i);
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                BookGenresVMs.Clear();
            }
        }
    }
}
