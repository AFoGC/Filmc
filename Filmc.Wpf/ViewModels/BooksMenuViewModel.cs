using Filmc.Wpf.Commands;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Models;
using Filmc.Xtl.Entities;
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

        private string _searchText;
        private bool _isReadedChecked;
        private bool _isUnReadedChecked;
        private BookViewModel? _selectedBook;

        private RelayCommand? changeMenuModeCommand;
        private RelayCommand? addCategoryCommand;
        private RelayCommand? addBookCommand;
        private RelayCommand? addBookInCategoryCommand;
        private RelayCommand? saveTablesCommand;
        private RelayCommand? filterCommand;
        private RelayCommand? selectCommand;
        private RelayCommand? addSelectedToCategory;
        private RelayCommand? removeSelectedFromCategory;
        private RelayCommand? addBookToPriorityCommand;
        private RelayCommand? deleteBookCommand;
        private RelayCommand? removeCategoryCommand;

        public BooksMenuViewModel(BooksModel model)
        {
            _model = model;
            TablesViewModel = new BookTablesViewModel(model);

            _searchText = String.Empty;
            _isReadedChecked = true;
            _isUnReadedChecked = true;
        }

        public BookTablesViewModel TablesViewModel { get; }

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); }
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

        public RelayCommand AddBookInCategoryCommand
        {
            get
            {
                return addBookInCategoryCommand ??
                (addBookInCategoryCommand = new RelayCommand(obj =>
                {
                    BookCategoryViewModel? categoryVM = obj as BookCategoryViewModel;

                    if (categoryVM != null)
                        _model.AddBook(categoryVM.Model.Id);
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
                    var selectedGenres = TablesViewModel.GenreVMs.Where(x => x.IsChecked);

                    foreach (var item in TablesViewModel.BooksVMs)
                    {
                        item.IsFiltered = IsBookPassingFilter(selectedGenres, item.Model);
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
                        categoryVM.Model.AddBookInOrder(SelectedBook.Model);
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
                        categoryVM.Model.RemoveBookInOrder(SelectedBook.Model);
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
                        if (_model.TablesContext.BookInPriorities.All(x => x.Id != viewModel.Model.Id))
                        {
                            BookInPriority priority = new BookInPriority() { Id = viewModel.Model.Id, CreationTime = DateTime.Now };
                            _model.TablesContext.BookInPriorities.Add(priority);
                        }
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

                        if (book.CategoryId != 0)
                            book.CategoryId = 0;

                        if (book.Priority != null)
                            _model.TablesContext.BookInPriorities.Remove(book.Priority);

                        _model.TablesContext.Books.Remove(book);
                    }
                }));
            }
        }

        private bool IsBookPassingFilter(IEnumerable<BookGenreViewModel> genres, Book book)
        {
            bool exp = false;

            if (genres.Any(x => x.Model == book.Genre))
                exp = book.IsReaded == IsReadedChecked || book.IsReaded != IsUnReadedChecked;

            return exp;
        }
    }
}
