using Filmc.Entities.Entities;
using Filmc.Wpf.Commands;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Helper;
using Filmc.Wpf.Services;
using Filmc.Wpf.SettingsServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.ViewModels
{
    public class SettingsMenuViewModel : BaseViewModel
    {
        private readonly GlobalSettingsService _settingsService;
        private readonly MarkSystemService _markSystemService;
        private readonly ExplorerService _explorerService;
        private readonly ImportFileDialogService _importFileDialogService;
        private readonly ChangeProfileWindowService _changeProfileWindowService;

        private string _newProfileName = String.Empty;

        private Profile? _selectedProfile;

        private static readonly char[] symbols = new char[]
        { '"', '\\', '/', ':', '|', '<', '>', '*', '?' };

        public SettingsMenuViewModel(GlobalSettingsService settingsService, MarkSystemService markSystemService,
                                     ExplorerService explorerService, ImportFileDialogService importFileService, 
                                     ChangeProfileWindowService changeProfileWindowService, BackgroundImageService backgroundImageService)
        {
            TablesViewModel = new SettingsTablesViewModel(settingsService);

            _settingsService = settingsService;
            _markSystemService = markSystemService;
            _explorerService = explorerService;
            _importFileDialogService = importFileService;
            _changeProfileWindowService = changeProfileWindowService;

            BackgroundImageViewModel = new BackgroundImageViewModel(backgroundImageService);

            OnSelectedProfileChanged(_settingsService.ProfilesService.SelectedProfile);
            _settingsService.ProfilesService.SelectedProfileChanged += OnSelectedProfileChanged;

            Timers = new List<double> { 10, 15, 30, 60, 360, 600 };
            MarkSystems = new List<int> { 3, 5, 6, 10, 12, 25 };

            AddFilmGenreCommand = new RelayCommand(AddFilmGenre);
            AddBookGenreCommand = new RelayCommand(AddBookGenre);
            DeleteFilmGenreCommand = new RelayCommand(DeleteFilmGenre);
            DeleteBookGenreCommand = new RelayCommand(DeleteBookGenre);
            AddFilmTagCommand = new RelayCommand(AddFilmTag);
            DeleteFilmTagCommand = new RelayCommand(DeleteFilmTag);
            AddBookTagCommand = new RelayCommand(AddBookTag);
            DeleteBookTagCommand = new RelayCommand(DeleteBookTag);
            AddFilmProgressCommand = new RelayCommand(AddFilmProgress);
            DeleteFilmProgressCommand = new RelayCommand(DeleteFilmProgress);
            AddBookProgressCommand = new RelayCommand(AddBookProgress);
            DeleteBookProgressCommand = new RelayCommand(DeleteBookProgress);
            ChangeProfileCommand = new RelayCommand(ChangeProfile);
            AddProfileCommand = new RelayCommand(AddProfile);
            DeleteProfileCommand = new RelayCommand(DeleteProfile);
            OpenExplorerCommand = new RelayCommand(OpenExplorer);
            ImportProfileCommand = new RelayCommand(ImportProfile);
        }

        public RelayCommand AddFilmGenreCommand { get; }
        public RelayCommand AddBookGenreCommand { get; }
        public RelayCommand DeleteFilmGenreCommand { get; }
        public RelayCommand DeleteBookGenreCommand { get; }
        public RelayCommand AddFilmTagCommand { get; }
        public RelayCommand DeleteFilmTagCommand { get; }
        public RelayCommand AddBookTagCommand { get; }
        public RelayCommand DeleteBookTagCommand { get; }
        public RelayCommand AddFilmProgressCommand { get; }
        public RelayCommand DeleteFilmProgressCommand { get; }
        public RelayCommand AddBookProgressCommand { get; }
        public RelayCommand DeleteBookProgressCommand { get; }
        public RelayCommand ChangeProfileCommand { get; }
        public RelayCommand AddProfileCommand { get; }
        public RelayCommand DeleteProfileCommand { get; }
        public RelayCommand OpenExplorerCommand { get; }
        public RelayCommand ImportProfileCommand { get; }

        public SettingsTablesViewModel TablesViewModel { get; }
        public BackgroundImageViewModel BackgroundImageViewModel { get; }
        public List<double> Timers { get; }
        public List<int> MarkSystems { get; }
        public IEnumerable<CultureInfo> Languages => _settingsService.LanguageService.Languages;

        public double SelectedTimer
        {
            get => _settingsService.AutoSaveService.SaveTimerInterval;
            set { _settingsService.AutoSaveService.SaveTimerInterval = value; OnPropertyChanged(); }
        }

        public bool TimerIsEnabled
        {
            get => _settingsService.AutoSaveService.IsAutosaveEnable;
            set { _settingsService.AutoSaveService.IsAutosaveEnable = value; OnPropertyChanged(); }
        }

        public int FilmMarkSystem
        {
            get => _markSystemService.FilmMarkSystem;
            set { _markSystemService.FilmMarkSystem = value; OnPropertyChanged(); }
        }

        public int BookMarkSystem
        {
            get => _markSystemService.BookMarkSystem;
            set { _markSystemService.BookMarkSystem = value; OnPropertyChanged(); }
        }

        public CultureInfo SelectedLanguage
        {
            get => _settingsService.LanguageService.CurrentLanguage;
            set { _settingsService.LanguageService.SetLanguage(value); OnPropertyChanged(); }
        }

        public int SelectedScaleIndex
        {
            get => (int)_settingsService.ScaleService.CurrentScale;
            set { _settingsService.ScaleService.SetScale(value); OnPropertyChanged(); }
        }

        public string NewProfileName
        {
            get => _newProfileName;
            set
            { 
                if (symbols.All(x => value.Contains(x) == false))
                {
                    _newProfileName = value;
                }

                OnPropertyChanged();
            }
        }

        private void OnSelectedProfileChanged(Profile profile)
        {
            _selectedProfile = profile;

            OnPropertyChanged(nameof(FilmMarkSystem));
            OnPropertyChanged(nameof(BookMarkSystem));
        }

        public void AddFilmGenre(object? obj)
        {
            var filmGenres = _selectedProfile!.TablesContext.FilmGenres;

            int i = filmGenres.Count + 1;
            FilmGenre filmGenre = new FilmGenre { Name = $"Genre{i}" };
            _selectedProfile?.TablesContext.FilmGenres.Add(filmGenre);
        }

        public void AddBookGenre(object? obj)
        {
            var bookGenres = _selectedProfile!.TablesContext.BookGenres;

            int i = bookGenres.Count + 1;
            BookGenre bookGenre = new BookGenre { Name = $"Genre{i}" };
            _selectedProfile?.TablesContext.BookGenres.Add(bookGenre);
        }

        public void DeleteFilmGenre(object? obj)
        {
            FilmGenreViewModel? genre = obj as FilmGenreViewModel;

            if (genre != null && genre.Model.Films.Count == 0)
                TablesViewModel.FilmGenres!.Remove(genre.Model);
        }

        public void DeleteBookGenre(object? obj)
        {
            BookGenreViewModel? genre = obj as BookGenreViewModel;

            if (genre != null && genre.Model.Books.Count == 0)
                TablesViewModel.BookGenres!.Remove(genre.Model);
        }

        public void AddFilmTag(object? obj)
        {
            var filmTags = _selectedProfile!.TablesContext.FilmTags;

            int i = filmTags.Count + 1;
            FilmTag filmTag = new FilmTag { Name = $"Tag{i}" };
            _selectedProfile?.TablesContext.FilmTags.Add(filmTag);
        }

        public void DeleteFilmTag(object? obj)
        {
            FilmTagViewModel? genre = obj as FilmTagViewModel;

            if (genre != null && genre.Model.Films.Count == 0)
                TablesViewModel.FilmTags!.Remove(genre.Model);
        }

        public void AddBookTag(object? obj)
        {
            var bookTags = _selectedProfile!.TablesContext.BookTags;

            int i = bookTags.Count + 1;
            BookTag bookTag = new BookTag { Name = $"Tag{i}" };
            _selectedProfile?.TablesContext.BookTags.Add(bookTag);
        }

        public void DeleteBookTag(object? obj)
        {
            BookTagViewModel? genre = obj as BookTagViewModel;

            if (genre != null && genre.Model.Books.Count == 0)
                TablesViewModel.BookTags!.Remove(genre.Model);
        }

        public void AddFilmProgress(object? obj)
        {
            var progresses = _selectedProfile!.TablesContext.FilmProgresses;

            int i = progresses.Count + 1;
            FilmWatchProgress progress = new FilmWatchProgress { Name = $"Name{i}" };
            _selectedProfile?.TablesContext.FilmProgresses.Add(progress);
        }

        public void DeleteFilmProgress(object? obj)
        {
            FilmWatchProgressViewModel? vm = obj as FilmWatchProgressViewModel;

            if (vm != null && vm.Model.Films.Count == 0)
                TablesViewModel.FilmProgresses!.Remove(vm.Model);
        }

        public void AddBookProgress(object? obj)
        {
            var progresses = _selectedProfile!.TablesContext.BookProgresses;

            int i = progresses.Count + 1;
            BookReadProgress progress = new BookReadProgress { Name = $"Name{i}" };
            _selectedProfile?.TablesContext.BookProgresses.Add(progress);
        }

        public void DeleteBookProgress(object? obj)
        {
            BookReadProgressViewModel? vm = obj as BookReadProgressViewModel;

            if (vm != null && vm.Model.Books.Count == 0)
                TablesViewModel.BookProgresses!.Remove(vm.Model);
        }

        public void ChangeProfile(object? obj)
        {
            ProfileViewModel? profileViewModel = obj as ProfileViewModel;
            if (profileViewModel != null)
            {
                if (_settingsService.ProfilesService.SelectedProfile.IsChangesSaved == false)
                {
                    _changeProfileWindowService.ShowDialog();

                    if (_changeProfileWindowService.Save)
                        _settingsService.ProfilesService.SelectedProfile.SaveTables();

                    if (_changeProfileWindowService.ChangeProfile)
                        _settingsService.ProfilesService.SelectedProfile = profileViewModel.Profile;
                }
                else
                {
                    _settingsService.ProfilesService.SelectedProfile = profileViewModel.Profile;
                }
            }
        }

        public void AddProfile(object? obj)
        {
            if (NewProfileName != String.Empty)
            {
                _settingsService.ProfilesService.CreateProfile(NewProfileName);
                NewProfileName = String.Empty;
            }
        }

        public void DeleteProfile(object? obj)
        {
            ProfileViewModel? profileViewModel = obj as ProfileViewModel;

            if (profileViewModel != null)
                _settingsService.ProfilesService.RemoveProfile(profileViewModel.Profile);
        }

        public void OpenExplorer(object? obj)
        {
            _explorerService.OpenExplorer(PathHelper.ProfilesPath);
        }

        public void ImportProfile(object? obj)
        {
            if (_importFileDialogService.OpenFileDialog())
            {
                string filePath = _importFileDialogService.FileName!;
                _settingsService.ProfilesService.ImportProfile(filePath);
            }
        }
    }
}
