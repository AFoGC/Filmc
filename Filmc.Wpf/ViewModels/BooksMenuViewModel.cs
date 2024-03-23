using Filmc.Entities.Entities;
using Filmc.Wpf.Commands;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Helper;
using Filmc.Wpf.Models;
using Filmc.Wpf.Services;
using Filmc.Wpf.SettingsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.ViewModels
{
    public class BooksMenuViewModel : BaseViewModel
    {
        private readonly BooksModel _model;
        private readonly AddEntityByUrlService _addEntityByUrlService;

        private string _searchText;

        private bool _isReadedChecked;
        private bool _isUnReadedChecked;
        private bool _isAllGenresChecked;
        private bool _isAllTagsChecked;

        private BookViewModel? _selectedBook;

        private RelayCommand? changeMenuModeCommand;
        private RelayCommand? addCategoryCommand;
        private RelayCommand? addBookCommand;
        private RelayCommand? addBookByUrlCommand;
        private RelayCommand? addBookInCategoryCommand;
        private RelayCommand? saveTablesCommand;
        private RelayCommand? filterCommand;
        private RelayCommand? selectCommand;
        private RelayCommand? addSelectedToCategory;
        private RelayCommand? removeSelectedFromCategory;
        private RelayCommand? addBookToPriorityCommand;
        private RelayCommand? deleteBookCommand;
        private RelayCommand? removeCategoryCommand;
        private RelayCommand? removeBookFromPriorityCommand;
        private RelayCommand? checkGenresCommand;
        private RelayCommand? checkTagsCommand;

        public BooksMenuViewModel(BooksModel model, UpdateMenuService updateMenuService, 
                                  BackgroundImageService backgroundImageService, AddEntityByUrlService addEntityByUrlService)
        {
            _model = model;
            _addEntityByUrlService = addEntityByUrlService;

            BackgroundImageViewModel = new BackgroundImageViewModel(backgroundImageService);
            TablesViewModel = new BookTablesViewModel(model, updateMenuService);

            _searchText = String.Empty;
            _isReadedChecked = true;
            _isUnReadedChecked = true;

            RefreshGenresChecked();
            RefreshTagsChecked();
        }

        public BookTablesViewModel TablesViewModel { get; }
        public BackgroundImageViewModel BackgroundImageViewModel { get; }

        public string SearchText
        {
            get => _searchText;
            set
            { 
                _searchText = value;

                string search = value.ToLowerInvariant();

                SearchInBooks(search);
                SearchInCategories(search);

                OnPropertyChanged(); 
            }
        }

        public bool IsReadedChecked
        {
            get => _isReadedChecked;
            set
            {
                if (IsUnReadedChecked != false || value != false)
                {
                    _isReadedChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsUnReadedChecked
        {
            get => _isUnReadedChecked;
            set
            {
                if (IsReadedChecked != false || value != false)
                {
                    _isUnReadedChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsAllGenresChecked
        {
            get => _isAllGenresChecked;
            set { _isAllGenresChecked = value; OnPropertyChanged(); }
        }

        public bool IsAllTagsChecked
        {
            get => _isAllTagsChecked;
            set { _isAllTagsChecked = value; OnPropertyChanged(); }
        }

        public BookViewModel? SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (_selectedBook != null)
                    _selectedBook.IsSelected = false;

                _selectedBook = value;

                if (_selectedBook != null)
                    _selectedBook.IsSelected = true;

                OnPropertyChanged();
            }
        }

        public RelayCommand ChangeMenuModeCommand
        {
            get
            {
                return changeMenuModeCommand ??
                (changeMenuModeCommand = new RelayCommand(obj =>
                {
                    TablesViewModel.MenuMode = (BooksMenuMode)obj;
                }));
            }
        }

        public RelayCommand AddCategoryCommand
        {
            get
            {
                return addCategoryCommand ??
                (addCategoryCommand = new RelayCommand(obj =>
                {
                    _model.AddCategory();
                }));
            }
        }

        public RelayCommand RemoveCategoryCommand
        {
            get
            {
                return removeCategoryCommand ??
                (removeCategoryCommand = new RelayCommand(obj =>
                {
                    BookCategoryViewModel? categoryVM = obj as BookCategoryViewModel;

                    if (categoryVM != null)
                    {
                        _model.RemoveCategory(categoryVM.Model);
                    }
                }));
            }
        }


        public RelayCommand AddBookCommand
        {
            get
            {
                return addBookCommand ??
                (addBookCommand = new RelayCommand(obj =>
                {
                    _model.AddBook();
                }));
            }
        }

        public RelayCommand AddBookByUrlCommand
        {
            get
            {
                return addBookByUrlCommand ??
                (addBookByUrlCommand = new RelayCommand(obj =>
                {
                    _addEntityByUrlService.CreateBookByUrl();
                }));
            }
        }

        public RelayCommand AddBookInCategoryCommand
        {
            get
            {
                return addBookInCategoryCommand ??
                (addBookInCategoryCommand = new RelayCommand(obj =>
                {
                    BookCategoryViewModel? categoryVM = obj as BookCategoryViewModel;

                    if (categoryVM != null)
                        _model.AddBook(categoryVM.Model);
                }));
            }
        }


        public RelayCommand SaveTablesCommand
        {
            get
            {
                return saveTablesCommand ??
                (saveTablesCommand = new RelayCommand(obj =>
                {
                    _model.SaveTables();
                }));
            }
        }


        public RelayCommand FilterCommand
        {
            get
            {
                return filterCommand ?? (filterCommand = new RelayCommand(obj =>
                {
                    RefreshGenresChecked();
                    RefreshTagsChecked();

                    var selectedGenres = TablesViewModel.GenreVMs.Where(x => x.IsChecked);
                    var selectedTags = TablesViewModel.TagVMs.Where(x => x.IsChecked);

                    foreach (var item in TablesViewModel.BooksVMs)
                    {
                        item.IsFiltered = IsBookPassingFilter(selectedTags, selectedGenres, item.Model);
                    }

                    foreach (var item in TablesViewModel.CategoryVMs)
                    {
                        item.IsFiltered = TablesViewModel.BooksVMs
                            .Where(x => x.Model.Category == item.Model)
                            .Any(x => x.IsFiltered);
                    }
                }));
            }
        }

        public RelayCommand SelectCommand
        {
            get
            {
                return selectCommand ??
                (selectCommand = new RelayCommand(obj =>
                {
                    BookViewModel? viewModel = obj as BookViewModel;
                    SelectedBook = viewModel;
                }));
            }
        }

        public RelayCommand AddSelectedToCategory
        {
            get
            {
                return addSelectedToCategory ??
                (addSelectedToCategory = new RelayCommand(obj =>
                {
                    BookCategoryViewModel? categoryVM = obj as BookCategoryViewModel;

                    if (categoryVM != null && SelectedBook != null)
                    {
                        _model.AddBookToCategory(categoryVM.Model, SelectedBook.Model);
                    }
                }));
            }
        }

        public RelayCommand RemoveSelectedFromCategory
        {
            get
            {
                return removeSelectedFromCategory ??
                (removeSelectedFromCategory = new RelayCommand(obj =>
                {
                    BookCategoryViewModel? categoryVM = obj as BookCategoryViewModel;

                    if (categoryVM != null && SelectedBook != null)
                    {
                        _model.RemoveBookFromCategory(categoryVM.Model, SelectedBook.Model);
                    }
                }));
            }
        }
        
        public RelayCommand AddBookToPriorityCommand
        {
            get
            {
                return addBookToPriorityCommand ??
                (addBookToPriorityCommand = new RelayCommand(obj =>
                {
                    BookViewModel? viewModel = obj as BookViewModel;

                    if (viewModel != null)
                    {
                        _model.AddBookToPriority(viewModel.Model);
                    }
                }));
            }
        }

        public RelayCommand RemoveBookFromPriorityCommand
        {
            get
            {
                return removeBookFromPriorityCommand ??
                (removeBookFromPriorityCommand = new RelayCommand(obj =>
                {
                    BookViewModel? viewModel = obj as BookViewModel;

                    if (viewModel != null)
                    {
                        _model.RemoveBookFromPriority(viewModel.Model);
                    }
                }));
            }
        }

        public RelayCommand DeleteBookCommand
        {
            get
            {
                return deleteBookCommand ??
                (deleteBookCommand = new RelayCommand(obj =>
                {
                    BookViewModel? viewModel = obj as BookViewModel;

                    if (viewModel != null)
                    {
                        Book book = viewModel.Model;
                        _model.DeleteBook(book);
                    }
                }));
            }
        }

        public RelayCommand CheckGenresCommand
        {
            get
            {
                return checkGenresCommand ??
                (checkGenresCommand = new RelayCommand(obj =>
                {
                    if (IsAllGenresChecked)
                    {
                        foreach (var vm in TablesViewModel.GenreVMs)
                            vm.IsChecked = false;
                    }
                    else
                    {
                        foreach (var vm in TablesViewModel.GenreVMs)
                            vm.IsChecked = true;
                    }

                    RefreshGenresChecked();
                    FilterCommand.Execute(obj);
                }));
            }
        }

        public RelayCommand CheckTagsCommand
        {
            get
            {
                return checkTagsCommand ??
                (checkTagsCommand = new RelayCommand(obj =>
                {
                    if (IsAllTagsChecked)
                    {
                        foreach (var vm in TablesViewModel.TagVMs)
                            vm.IsChecked = false;
                    }
                    else
                    {
                        foreach (var vm in TablesViewModel.TagVMs)
                            vm.IsChecked = true;
                    }

                    RefreshTagsChecked();
                    FilterCommand.Execute(obj);
                }));
            }
        }

        private void RefreshGenresChecked()
        {
            IsAllGenresChecked = TablesViewModel.GenreVMs.All(x => x.IsChecked);
        }

        private void RefreshTagsChecked()
        {
            IsAllTagsChecked = TablesViewModel.TagVMs.All(x => x.IsChecked);
        }

        private void SearchInBooks(string search)
        {
            foreach (var viewModel in TablesViewModel.BooksVMs)
                viewModel.IsFinded = false;

            var filtredVMs = TablesViewModel.BooksVMs
                .Where(x => x.Name.SearchBy(search));

            foreach (var viewModel in filtredVMs)
                viewModel.IsFinded = true;
        }

        private void SearchInCategories(string search)
        {
            foreach (var viewModel in TablesViewModel.CategoryVMs)
                viewModel.IsFinded = false;

            var filtredVMs = TablesViewModel.CategoryVMs
                .Where(x => x.Model.Name.SearchBy(search) || x.Model.Books.Any(y => y.Name.SearchBy(search)));

            foreach (var viewModel in filtredVMs)
                viewModel.IsFinded = true;
        }

        private bool IsBookPassingFilter(IEnumerable<BookTagViewModel> tags, IEnumerable<BookGenreViewModel> genres, Book book)
        {
            bool readedPassed = false;
            bool genresPassed = false;
            bool tagsPassed = false;

            readedPassed = book.IsReaded == IsReadedChecked || book.IsReaded != IsUnReadedChecked;
            genresPassed = genres.Any(x => x.Model == book.Genre);

            if (tags.Count() != TablesViewModel.TagVMs.Count)
            {
                var ft = tags.Select(i => i.Model);
                tagsPassed = book.Tags.IntersectBy(ft, x => x).Any();
            }
            else
            {
                tagsPassed = true;
            }

            return readedPassed && genresPassed && tagsPassed;
        }
    }
}
