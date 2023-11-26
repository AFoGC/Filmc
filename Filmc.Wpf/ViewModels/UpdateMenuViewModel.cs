using Filmc.Wpf.Commands;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Services;
using Filmc.Wpf.SettingsServices;
using Filmc.Xtl.Entities;
using Filmc.Xtl.EntityProperties;
using Filmc.Xtl.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Filmc.Wpf.ViewModels
{
    public class UpdateMenuViewModel : BaseViewModel
    {
        private readonly MarkSystemService _markSystemService;
        private readonly ProfilesService _profilesService;
        private readonly Regex _numericRegex;

        private BaseEntityViewModel? _currentEntityViewModel;
        private List<int?>? _filmMarks;
        private List<int?>? _bookMarks;

        private RelayCommand? removeTagCommand;
        private RelayCommand? closeMenuCommand;
        private RelayCommand? addSource;
        private RelayCommand? removeSource;
        private RelayCommand? setFirstSource;
        private RelayCommand? checkIsTextCommand;

        public UpdateMenuViewModel(MarkSystemService markSystemService, ProfilesService profilesService)
        {
            _markSystemService = markSystemService;
            _profilesService = profilesService;
            _numericRegex = new Regex("[^0-9.-]+");

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
        public FilmTagsTable FilmTags => _profilesService.SelectedProfile.TablesContext.FilmTags;
        public BookTagsTable BookTags => _profilesService.SelectedProfile.TablesContext.BookTags;

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

        public object? SelectedTag
        {
            get => null;
            set
            {
                FilmTag? filmTag = value as FilmTag;
                FilmViewModel? filmViewModel = _currentEntityViewModel as FilmViewModel;

                if (filmTag != null && filmViewModel != null)
                {
                    if (filmViewModel.Model.HasTags.Any(x => x.Tag == filmTag) == false)
                    {
                        FilmHasTag filmHasTag = new FilmHasTag();

                        _profilesService.SelectedProfile.TablesContext.FilmHasTags.Add(filmHasTag);
                        filmHasTag.FilmId = filmViewModel.Model.Id;
                        filmHasTag.TagId = filmTag.Id;
                        OnPropertyChanged();
                    }
                    return;
                }

                BookTag? bookTag = value as BookTag;
                BookViewModel? bookViewModel = _currentEntityViewModel as BookViewModel;

                if (bookTag != null && bookViewModel != null)
                {
                    if (bookViewModel.Model.HasTags.Any(x => x.Tag == bookTag) == false)
                    {
                        BookHasTag bookHasTag = new BookHasTag();

                        _profilesService.SelectedProfile.TablesContext.BookHasTags.Add(bookHasTag);
                        bookHasTag.BookId = bookViewModel.Model.Id;
                        bookHasTag.TagId = bookTag.Id;
                        OnPropertyChanged();
                    }
                    return;
                }
            }
        }

        public RelayCommand RemoveTagMenuCommand
        {
            get
            {
                return removeTagCommand ??
                (removeTagCommand = new RelayCommand(obj =>
                {
                    FilmHasTag? filmHasTag = obj as FilmHasTag;
                    FilmViewModel? filmViewModel = _currentEntityViewModel as FilmViewModel;

                    if (filmHasTag != null && filmViewModel != null)
                    {
                        if (filmViewModel.Model.HasTags.Contains(filmHasTag))
                        {
                            _profilesService.SelectedProfile.TablesContext.FilmHasTags.Remove(filmHasTag);
                        }
                        return;
                    }

                    BookHasTag? bookHasTag = obj as BookHasTag;
                    BookViewModel? bookViewModel = _currentEntityViewModel as BookViewModel;

                    if (bookHasTag != null && bookViewModel != null)
                    {
                        if (bookViewModel.Model.HasTags.Contains(bookHasTag))
                        {
                            _profilesService.SelectedProfile.TablesContext.BookHasTags.Remove(bookHasTag);
                        }
                        return;
                    }
                }));
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

        public RelayCommand CheckIsTextCommand
        {
            get
            {
                return checkIsTextCommand ??
                (checkIsTextCommand = new RelayCommand(obj =>
                {
                    TextCompositionEventArgs? e = obj as TextCompositionEventArgs;

                    if (e != null)
                    {
                        e.Handled = _numericRegex.IsMatch(e.Text);
                    }
                }));
            }
        }

        private void OnSelectedProfileChanged(Profile obj)
        {
            OnPropertyChanged(nameof(FilmGenres));
            OnPropertyChanged(nameof(BookGenres));
            OnPropertyChanged(nameof(FilmTags));
            OnPropertyChanged(nameof(BookTags));
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
