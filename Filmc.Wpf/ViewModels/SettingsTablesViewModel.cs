using Filmc.Entities.Entities;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.Services;
using Filmc.Wpf.SettingsServices;
using Filmc.Wpf.ViewCollections;
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
        private FilmTagCategoryRepository? _filmTagCategories;
        private BookTagCategoryRepository? _bookTagCategories;
        private FilmWatchProgressRepository? _filmProgresses;
        private BookReadProgressRepository? _bookProgresses;

        private readonly EntityObserver<FilmGenre, FilmGenreViewModel> _filmGenreEntityObserver;
        private readonly EntityObserver<BookGenre, BookGenreViewModel> _bookGenreEntityObserver;
        private readonly EntityObserver<FilmTag, FilmTagSettingsViewModel> _filmTagEntityObserver;
        private readonly EntityObserver<BookTag, BookTagSettingsViewModel> _bookTagEntityObserver;
        private readonly EntityObserver<FilmTagCategory, FilmTagCategorySettingsViewModel> _filmTagCategoryEntityObserver;
        private readonly EntityObserver<BookTagCategory, BookTagCategorySettingsViewModel> _bookTagCategoryEntityObserver;
        private readonly EntityObserver<FilmWatchProgress, FilmWatchProgressViewModel> _filmProgressEntityObserver;
        private readonly EntityObserver<BookReadProgress, BookReadProgressViewModel> _bookProgressEntityObserver;

        public SettingsTablesViewModel(GlobalSettingsService settingsService)
        {
            _profilesService = settingsService.ProfilesService;

            ProfileVMs = new ObservableCollection<ProfileViewModel>();
            FilmGenresVMs = new ObservableCollection<FilmGenreViewModel>();
            BookGenresVMs = new ObservableCollection<BookGenreViewModel>();
            FilmTagsVMs = new ObservableCollection<FilmTagSettingsViewModel>();
            BookTagsVMs = new ObservableCollection<BookTagSettingsViewModel>();
            FilmTagCategoriesVMs = new ObservableCollection<FilmTagCategorySettingsViewModel>();
            BookTagCategoriesVMs = new ObservableCollection<BookTagCategorySettingsViewModel>();
            FilmProgressVMs = new ObservableCollection<FilmWatchProgressViewModel>();
            BookProgressVMs = new ObservableCollection<BookReadProgressViewModel>();

            _filmGenreEntityObserver = new EntityObserver<FilmGenre, FilmGenreViewModel>(FilmGenresVMs, CreateFilmGenre);
            _bookGenreEntityObserver = new EntityObserver<BookGenre, BookGenreViewModel>(BookGenresVMs, CreateBookGenre);
            _filmTagEntityObserver = new EntityObserver<FilmTag, FilmTagSettingsViewModel>(FilmTagsVMs, CreateFilmTag);
            _bookTagEntityObserver = new EntityObserver<BookTag, BookTagSettingsViewModel>(BookTagsVMs, CreateBookTag);
            _filmTagCategoryEntityObserver = new EntityObserver<FilmTagCategory, FilmTagCategorySettingsViewModel>(FilmTagCategoriesVMs, CreateFilmTagCategory);
            _bookTagCategoryEntityObserver = new EntityObserver<BookTagCategory, BookTagCategorySettingsViewModel>(BookTagCategoriesVMs, CreateBookTagCategory);
            _filmProgressEntityObserver = new EntityObserver<FilmWatchProgress, FilmWatchProgressViewModel>(FilmProgressVMs, CreateFilmWatchProgress);
            _bookProgressEntityObserver = new EntityObserver<BookReadProgress, BookReadProgressViewModel>(BookProgressVMs, CreateBookReadProgress);

            ProfilesInit();
            OnProfileChanged(_profilesService.SelectedProfile);
            _profilesService.SelectedProfileChanged += OnProfileChanged;
            _profilesService.ProfileAdded += OnProfileAdded;
            _profilesService.ProfileRemoved += ProfileRemoved;
        }

        public ObservableCollection<ProfileViewModel> ProfileVMs { get; }
        public ObservableCollection<FilmGenreViewModel> FilmGenresVMs { get; }
        public ObservableCollection<BookGenreViewModel> BookGenresVMs { get; }
        public ObservableCollection<FilmTagSettingsViewModel> FilmTagsVMs { get; }
        public ObservableCollection<BookTagSettingsViewModel> BookTagsVMs { get; }
        public ObservableCollection<FilmTagCategorySettingsViewModel> FilmTagCategoriesVMs { get; }
        public ObservableCollection<BookTagCategorySettingsViewModel> BookTagCategoriesVMs { get; }
        public ObservableCollection<FilmWatchProgressViewModel> FilmProgressVMs { get; }
        public ObservableCollection<BookReadProgressViewModel> BookProgressVMs { get; }

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
        public FilmTagCategoryRepository? FilmTagCategories
        {
            get => _filmTagCategories;
            set { _filmTagCategories = value; OnPropertyChanged(); }
        }
        public BookTagCategoryRepository? BookTagCategories
        {
            get => _bookTagCategories;
            set { _bookTagCategories = value; OnPropertyChanged(); }
        }
        public FilmWatchProgressRepository? FilmProgresses
        {
            get => _filmProgresses;
            set { _filmProgresses = value; OnPropertyChanged(); }
        }
        public BookReadProgressRepository? BookProgresses
        {
            get => _bookProgresses;
            set { _bookProgresses = value; OnPropertyChanged(); }
        }

        private void ProfilesInit()
        {
            foreach (var profile in _profilesService.Profiles)
                ProfileVMs.Add(new ProfileViewModel(profile));
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

            FilmGenres = newProfile.TablesContext.FilmGenres;
            BookGenres = newProfile.TablesContext.BookGenres;
            FilmTags = newProfile.TablesContext.FilmTags;
            BookTags = newProfile.TablesContext.BookTags;
            FilmTagCategories = newProfile.TablesContext.FilmTagCategories;
            BookTagCategories = newProfile.TablesContext.BookTagCategories;
            FilmProgresses = newProfile.TablesContext.FilmProgresses;
            BookProgresses = newProfile.TablesContext.BookProgresses;

            _filmGenreEntityObserver.SetSource(FilmGenres);
            _bookGenreEntityObserver.SetSource(BookGenres);
            _filmTagEntityObserver.SetSource(FilmTags);
            _bookTagEntityObserver.SetSource(BookTags);
            _filmTagCategoryEntityObserver.SetSource(FilmTagCategories);
            _bookTagCategoryEntityObserver.SetSource(BookTagCategories);
            _filmProgressEntityObserver.SetSource(FilmProgresses);
            _bookProgressEntityObserver.SetSource(BookProgresses);
        }

        private FilmGenreViewModel CreateFilmGenre(FilmGenre entity)
        {
            return new FilmGenreViewModel(entity);
        }

        private BookGenreViewModel CreateBookGenre(BookGenre entity)
        {
            return new BookGenreViewModel(entity);
        }

        private FilmTagSettingsViewModel CreateFilmTag(FilmTag entity)
        {
            return new FilmTagSettingsViewModel(entity, FilmTagCategoriesVMs);
        }

        private BookTagSettingsViewModel CreateBookTag(BookTag entity)
        {
            return new BookTagSettingsViewModel(entity, BookTagCategoriesVMs);
        }

        private FilmTagCategorySettingsViewModel CreateFilmTagCategory(FilmTagCategory entity)
        {
            return new FilmTagCategorySettingsViewModel(entity);
        }

        private BookTagCategorySettingsViewModel CreateBookTagCategory(BookTagCategory entity)
        {
            return new BookTagCategorySettingsViewModel(entity);
        }

        private FilmWatchProgressViewModel CreateFilmWatchProgress(FilmWatchProgress entity)
        {
            return new FilmWatchProgressViewModel(entity);
        }

        private BookReadProgressViewModel CreateBookReadProgress(BookReadProgress entity)
        {
            return new BookReadProgressViewModel(entity);
        }
    }
}
