using Filmc.Wpf.Commands;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Models;
using Filmc.Wpf.ViewCollections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filmc.Wpf.Services;
using Filmc.Wpf.Repositories;
using Filmc.Entities.Entities;

namespace Filmc.Wpf.ViewModels
{
    public class BookTablesViewModel : BaseViewModel
    {
        private readonly BooksModel _model;

        private readonly UpdateMenuService _updateMenuService;

        private readonly EntityObserver<Book, BookViewModel> _bookEntityObserver;
        private readonly EntityObserver<BookCategory, BookCategoryViewModel> _categoryEntityObserver;
        private readonly EntityObserver<BookGenre, BookGenreViewModel> _genreEntityObserver;
        private readonly EntityObserver<BookTag, BookTagViewModel> _tagEntityObserver;

        private RepositoriesFacade? _tablesContext;
        private BooksMenuMode _menuMode;

        private RelayCommand? sortTable;

        public BookTablesViewModel(BooksModel model, UpdateMenuService updateMenuService)
        {
            _menuMode = BooksMenuMode.Categories;

            BooksVMs = new ObservableCollection<BookViewModel>();
            CategoryVMs = new ObservableCollection<BookCategoryViewModel>();
            GenreVMs = new ObservableCollection<BookGenreViewModel>();
            TagVMs = new ObservableCollection<BookTagViewModel>();

            _bookEntityObserver = new EntityObserver<Book, BookViewModel>(BooksVMs, CreateFilmViewModel);
            _categoryEntityObserver = new EntityObserver<BookCategory, BookCategoryViewModel>(CategoryVMs, CreateCategoryViewModel);
            _genreEntityObserver = new EntityObserver<BookGenre, BookGenreViewModel>(GenreVMs, CreateGenreViewModel);
            _tagEntityObserver = new EntityObserver<BookTag, BookTagViewModel>(TagVMs, CreateTagViewModel);

            _model = model;
            _updateMenuService = updateMenuService;
            _model.TablesContextChanged += OnTablesContextChanged;
            OnTablesContextChanged();

            CategoriesVC = new BookCategoriesViewCollection(CategoryVMs);
            BooksSimplifiedVC = new BooksSimplifiedViewCollection(BooksVMs);
            BooksVC = new BooksViewCollection(BooksVMs);
            PrioritiesVC = new BooksInPriorityViewCollection(BooksVMs);

            CategoriesVC.ChangeSortProperty("Id");
            BooksSimplifiedVC.ChangeSortProperty("Id");
            BooksVC.ChangeSortProperty("Id");
        }

        public ObservableCollection<BookViewModel> BooksVMs { get; }
        public ObservableCollection<BookCategoryViewModel> CategoryVMs { get; }
        public ObservableCollection<BookGenreViewModel> GenreVMs { get; }
        public ObservableCollection<BookTagViewModel> TagVMs { get; }

        public BookCategoriesViewCollection CategoriesVC { get; }
        public BooksSimplifiedViewCollection BooksSimplifiedVC { get; }
        public BooksViewCollection BooksVC { get; }
        public BooksInPriorityViewCollection PrioritiesVC { get; }

        public BooksMenuMode MenuMode
        {
            get => _menuMode;
            set { _menuMode = value; OnPropertyChanged(); }
        }

        private void OnTablesContextChanged()
        {
            _tablesContext = _model.TablesContext;

            _bookEntityObserver.SetSource(_tablesContext.Books);
            _categoryEntityObserver.SetSource(_tablesContext.BookCategories);
            _genreEntityObserver.SetSource(_tablesContext.BookGenres);
            _tagEntityObserver.SetSource(_tablesContext.BookTags);
        }

        public RelayCommand SortTable
        {
            get
            {
                return sortTable ?? (sortTable = new RelayCommand(obj =>
                {
                    string str = obj as string;

                    switch (MenuMode)
                    {
                        case BooksMenuMode.Categories:
                            CategoriesVC.ChangeSortProperty(str);
                            BooksSimplifiedVC.ChangeSortProperty(str);
                            break;

                        case BooksMenuMode.Books:
                            BooksVC.ChangeSortProperty(str);
                            break;

                        case BooksMenuMode.Priorities:
                            PrioritiesVC.ChangeSortProperty(str);
                            break;
                    }
                }));
            }
        }

        private BookViewModel CreateFilmViewModel(Book book)
        {
            return new BookViewModel(book, _updateMenuService);
        }

        private BookCategoryViewModel CreateCategoryViewModel(BookCategory bookCategory)
        {
            return new BookCategoryViewModel(bookCategory, BooksVMs, _updateMenuService, _tablesContext);
        }

        private BookGenreViewModel CreateGenreViewModel(BookGenre bookGenre)
        {
            return new BookGenreViewModel(bookGenre);
        }

        private BookTagViewModel CreateTagViewModel(BookTag bookTag)
        {
            return new BookTagViewModel(bookTag);
        }
    }
}
