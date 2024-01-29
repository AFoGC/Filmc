using Filmc.Entities.Entities;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.Services;
using Filmc.Wpf.SettingsServices;
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

        private FilmGenreRepository? _filmGenres;
        private BookGenreRepository? _bookGenres;
        private FilmTagRepository? _filmTags;
        private BookTagRepository? _bookTags;

        public SettingsTablesViewModel(SettingsService settingsService)
        {
            _profilesService = settingsService.ProfilesService;

            ProfileVMs = new ObservableCollection<ProfileViewModel>();
            FilmGenresVMs = new ObservableCollection<FilmGenreViewModel>();
            BookGenresVMs = new ObservableCollection<BookGenreViewModel>();
            FilmTagsVMs = new ObservableCollection<FilmTagViewModel>();
            BookTagsVMs = new ObservableCollection<BookTagViewModel>();

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
        public ObservableCollection<FilmTagViewModel> FilmTagsVMs { get; }
        public ObservableCollection<BookTagViewModel> BookTagsVMs { get; }

        public FilmGenreRepository? FilmGenres
        {
            get => _filmGenres;
            set { _filmGenres = value; OnPropertyChanged(); }
        }
        public BookGenreRepository? BookGenres
        {
            get => _bookGenres;
            set { _bookGenres = value; OnPropertyChanged(); }
        }
        public FilmTagRepository? FilmTags
        {
            get => _filmTags;
            set { _filmTags = value; OnPropertyChanged(); }
        }
        public BookTagRepository? BookTags
        {
            get => _bookTags;
            set { _bookTags = value; OnPropertyChanged(); }
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

            if (FilmTags != null)
                FilmTags.CollectionChanged -= OnFilmTagsCollectionChanged;

            if (BookTags != null)
                BookTags.CollectionChanged -= OnBookTagsCollectionChanged;

            FilmGenres = newProfile.TablesContext.FilmGenres;
            BookGenres = newProfile.TablesContext.BookGenres;
            FilmTags = newProfile.TablesContext.FilmTags;
            BookTags = newProfile.TablesContext.BookTags;

            FilmGenresVMs.Clear();
            foreach (var item in FilmGenres)
                FilmGenresVMs.Add(new FilmGenreViewModel(item));

            BookGenresVMs.Clear();
            foreach (var item in BookGenres)
                BookGenresVMs.Add(new BookGenreViewModel(item));

            FilmTagsVMs.Clear();
            foreach (var item in FilmTags)
                FilmTagsVMs.Add(new FilmTagViewModel(item));

            BookTagsVMs.Clear();
            foreach (var item in BookTags)
                BookTagsVMs.Add(new BookTagViewModel(item));

            FilmGenres.CollectionChanged += OnFilmGenresCollectionChanged;
            BookGenres.CollectionChanged += OnBookGenresCollectionChanged;
            FilmTags.CollectionChanged += OnFilmTagsCollectionChanged;
            BookTags.CollectionChanged += OnBookTagsCollectionChanged;
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
                BookGenresVMs.Insert(e.NewStartingIndex, new BookGenreViewModel(entity));
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

        private void OnFilmTagsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                FilmTag entity = (FilmTag)e.NewItems[0]!;
                FilmTagsVMs.Insert(e.NewStartingIndex, new FilmTagViewModel(entity));
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                int i = e.OldStartingIndex;
                FilmTagsVMs.RemoveAt(i);
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                FilmTagsVMs.Clear();
            }
        }

        private void OnBookTagsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                BookTag entity = (BookTag)e.NewItems[0]!;
                BookTagsVMs.Insert(e.NewStartingIndex, new BookTagViewModel(entity));
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                int i = e.OldStartingIndex;
                BookTagsVMs.RemoveAt(i);
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                BookTagsVMs.Clear();
            }
        }
    }
}
