using Filmc.Wpf.Commands;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Helper;
using Filmc.Wpf.Services;
using Filmc.Wpf.SettingsServices;
using Filmc.Xtl.Entities;
using Filmc.Xtl.Tables;
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
        private readonly SettingsService _settingsService;
        private readonly MarkSystemService _markSystemService;
        private readonly ExplorerService _explorerService;
        private readonly ImportFileDialogService _importFileDialogService;
        private readonly ChangeProfileWindowService _changeProfileWindowService;

        private string _newProfileName = String.Empty;

        private Profile? _selectedProfile;

        private RelayCommand? addFilmGenreCommand;
        private RelayCommand? addBookGenreCommand;
        private RelayCommand? deleteFilmGenreCommand;
        private RelayCommand? deleteBookGenreCommand;
        private RelayCommand? changeProfileCommand;
        private RelayCommand? addProfileCommand;
        private RelayCommand? deleteProfileCommand;
        private RelayCommand? importProfileCommand;
        private RelayCommand? openExplorerCommand;

        private static readonly char[] symbols = new char[]
        { '"', '\\', '/', ':', '|', '<', '>', '*', '?' };

        public SettingsMenuViewModel(SettingsService settingsService, MarkSystemService markSystemService,
                                     ExplorerService explorerService, ImportFileDialogService importFileService, 
                                     ChangeProfileWindowService changeProfileWindowService)
        {
            TablesViewModel = new SettingsTablesViewModel(settingsService);

            _settingsService = settingsService;
            _markSystemService = markSystemService;
            _explorerService = explorerService;
            _importFileDialogService = importFileService;
            _changeProfileWindowService = changeProfileWindowService;

            OnSelectedProfileChanged(_settingsService.ProfilesService.SelectedProfile);
            _settingsService.ProfilesService.SelectedProfileChanged += OnSelectedProfileChanged;

            Timers = new List<double> { 10, 15, 30, 60, 360, 600 };
            MarkSystems = new List<int> { 3, 5, 6, 10, 12, 25 };
            _changeProfileWindowService = changeProfileWindowService;
        }

        public SettingsTablesViewModel TablesViewModel { get; }
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

        public RelayCommand AddFilmGenreCommand
        {
            get
            {
                return addFilmGenreCommand ??
                (addFilmGenreCommand = new RelayCommand(obj =>
                {
                    var filmGenres = _selectedProfile!.TablesContext.FilmGenres;

                    int i = filmGenres.Count + 1;
                    FilmGenre filmGenre = new FilmGenre { Name = $"Genre{i}" };
                    _selectedProfile?.TablesContext.FilmGenres.Add(filmGenre);
                }));
            }
        }

        public RelayCommand AddBookGenreCommand
        {
            get
            {
                return addBookGenreCommand ??
                (addBookGenreCommand = new RelayCommand(obj =>
                {
                    var bookGenres = _selectedProfile!.TablesContext.BookGenres;

                    int i = bookGenres.Count + 1;
                    BookGenre bookGenre = new BookGenre { Name = $"Genre{i}" };
                    _selectedProfile?.TablesContext.BookGenres.Add(bookGenre);
                }));
            }
        }

        public RelayCommand DeleteFilmGenreCommand
        {
            get
            {
                return deleteFilmGenreCommand ??
                (deleteFilmGenreCommand = new RelayCommand(obj =>
                {
                    FilmGenreViewModel? genre = obj as FilmGenreViewModel;

                    if (genre != null && genre.Model.Films.Count == 0)
                        TablesViewModel.FilmGenres!.Remove(genre.Model);
                }));
            }
        }

        public RelayCommand DeleteBookGenreCommand
        {
            get
            {
                return deleteBookGenreCommand ??
                (deleteBookGenreCommand = new RelayCommand(obj =>
                {
                    BookGenreViewModel? genre = obj as BookGenreViewModel;

                    if (genre != null && genre.Model.Books.Count == 0)
                        TablesViewModel.BookGenres!.Remove(genre.Model);
                }));
            }
        }

        public RelayCommand ChangeProfileCommand
        {
            get
            {
                return changeProfileCommand ??
                (changeProfileCommand = new RelayCommand(obj =>
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
                }));
            }
        }

        public RelayCommand AddProfileCommand
        {
            get
            {
                return addProfileCommand ??
                (addProfileCommand = new RelayCommand(obj =>
                {
                    if (NewProfileName != String.Empty)
                    {
                        _settingsService.ProfilesService.CreateProfile(NewProfileName);
                        NewProfileName = String.Empty;
                    }
                }));
            }
        }

        public RelayCommand DeleteProfileCommand
        {
            get
            {
                return deleteProfileCommand ??
                (deleteProfileCommand = new RelayCommand(obj =>
                {
                    ProfileViewModel? profileViewModel = obj as ProfileViewModel;

                    if (profileViewModel != null)
                        _settingsService.ProfilesService.RemoveProfile(profileViewModel.Profile);
                }));
            }
        }

        public RelayCommand OpenExplorerCommand
        {
            get
            {
                return openExplorerCommand ??
                (openExplorerCommand = new RelayCommand(obj =>
                {
                    _explorerService.OpenExplorer(PathHelper.ProfilesPath);
                }));
            }
        }

        public RelayCommand ImportProfileCommand
        {
            get
            {
                return importProfileCommand ??
                (importProfileCommand = new RelayCommand(obj =>
                {
                    if (_importFileDialogService.OpenFileDialog())
                    {
                        string filePath = _importFileDialogService.FileName!;
                        _settingsService.ProfilesService.ImportProfile(filePath);
                    }
                }));
            }
        }
    }
}
