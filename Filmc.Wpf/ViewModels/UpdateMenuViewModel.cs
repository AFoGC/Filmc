using Filmc.Wpf.Commands;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Services;
using Filmc.Wpf.SettingsServices;
using Filmc.Xtl.EntityProperties;
using Filmc.Xtl.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Filmc.Wpf.ViewModels
{
    public class UpdateMenuViewModel : BaseViewModel
    {
        private readonly MarkSystemService _markSystemService;
        private readonly ProfilesService _profilesService;

        private BaseEntityViewModel? _currentEntityViewModel;
        private List<int?>? _filmMarks;
        private List<int?>? _bookMarks;

        private RelayCommand? closeMenuCommand;
        private RelayCommand? addSource;
        private RelayCommand? removeSource;
        private RelayCommand? setFirstSource;

        public UpdateMenuViewModel(MarkSystemService markSystemService, ProfilesService profilesService)
        {
            _markSystemService = markSystemService;
            _profilesService = profilesService;

            _profilesService.SelectedProfileChanged += OnSelectedProfileChanged;

            _markSystemService.FilmsMarkSystemChanged += OnFilmsMarkSystemChanged;
            _markSystemService.BooksMarkSystemChanged += OnBooksMarkSystemChanged;

            OnFilmsMarkSystemChanged();
            OnBooksMarkSystemChanged();
        }

        public bool IsVisible => _currentEntityViewModel != null;
        public Type? CurrentEntityViewModelType => CurrentEntityViewModel?.GetType();

        public int FilmMarkSystem => _markSystemService.FilmMarkSystem;
        public int BookMarkSystem => _markSystemService.BookMarkSystem;

        public FilmGenresTable FilmGenres => _profilesService.SelectedProfile.TablesContext.FilmGenres;
        public BookGenresTable BookGenres => _profilesService.SelectedProfile.TablesContext.BookGenres;

        public List<int?>? FilmMarks
        {
            get => _filmMarks;
            set { _filmMarks = value; OnPropertyChanged(); }
        }

        public List<int?>? BookMarks
        {
            get => _bookMarks;
            set { _bookMarks = value; OnPropertyChanged(); }
        }

        public Visibility MenuVisibility
        {
            get
            {
                Visibility visibility = Visibility.Hidden;

                if (IsVisible)
                    visibility = Visibility.Visible;

                return visibility;
            }
        }

        public BaseEntityViewModel? CurrentEntityViewModel
        {
            get => _currentEntityViewModel;
            set
            { 
                _currentEntityViewModel = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsVisible));
                OnPropertyChanged(nameof(MenuVisibility));
            }
        }

        public RelayCommand CloseMenuCommand
        {
            get
            {
                return closeMenuCommand ??
                (closeMenuCommand = new RelayCommand(obj =>
                {
                    CloseMenu();
                }));
            }
        }

        public RelayCommand AddSource
        {
            get
            {
                return addSource ??
                (addSource = new RelayCommand(obj =>
                {
                    IHasSourcesViewModel? viewModel = CurrentEntityViewModel as IHasSourcesViewModel;

                    if (viewModel != null)
                    {
                        viewModel.Sources.Add(new Source());
                    }
                }));
            }
        }

        public RelayCommand RemoveSource
        {
            get
            {
                return removeSource ??
                (removeSource = new RelayCommand(obj =>
                {
                    Source? source = obj as Source;
                    IHasSourcesViewModel? viewModel = CurrentEntityViewModel as IHasSourcesViewModel;

                    if (viewModel != null)
                        if (source != null)
                            viewModel.Sources.Remove(source);
                }));
            }
        }

        public RelayCommand SetFirstSource
        {
            get
            {
                return setFirstSource ??
                (setFirstSource = new RelayCommand(obj =>
                {
                    IHasSourcesViewModel? viewModel = CurrentEntityViewModel as IHasSourcesViewModel;
                    Source? source = obj as Source;

                    if (viewModel != null)
                    {
                        if (source != null)
                        {
                            viewModel.Sources.Remove(source);
                            viewModel.Sources.Insert(0, source);
                        }
                    }
                }));
            }
        }

        private void OnSelectedProfileChanged(Profile obj)
        {
            OnPropertyChanged(nameof(FilmGenres));
            OnPropertyChanged(nameof(BookGenres));
        }

        private void OnFilmsMarkSystemChanged()
        {
            List<int?> marks = new List<int?>();

            int i = _markSystemService.FilmMarkSystem;

            do
            {
                marks.Add(i);
                i--;
            }
            while (i != 0);

            FilmMarks = marks;
            OnPropertyChanged(nameof(FilmMarkSystem));
        }

        private void OnBooksMarkSystemChanged()
        {
            List<int?> marks = new List<int?>();

            int i = _markSystemService.BookMarkSystem;

            do
            {
                marks.Add(i);
                i--;
            }
            while (i != 0);

            BookMarks = marks;
            OnPropertyChanged(nameof(BookMarkSystem));
        }

        public void CloseMenu()
        {
            CurrentEntityViewModel = null;
        }
    }
}
