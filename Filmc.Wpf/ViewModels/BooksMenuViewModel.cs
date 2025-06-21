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
        private readonly AddEntityWindowService _addEntityWindowService;

        private string _searchText;

        private bool _isReadedChecked;
        private bool _isUnReadedChecked;
        private bool _isAllGenresChecked;
        private bool _isAllTagsChecked;
        private bool _isAllProgressesChecked;

        private BookViewModel? _selectedBook;

        public BooksMenuViewModel(BooksModel model, UpdateMenuService updateMenuService, 
                                  BackgroundImageService backgroundImageService, AddEntityWindowService addEntityWindowService)
        {
            _model = model;
            _addEntityWindowService = addEntityWindowService;

            BackgroundImageViewModel = new BackgroundImageViewModel(backgroundImageService);
            TablesViewModel = new BookTablesViewModel(model, updateMenuService);

            _searchText = String.Empty;
            _isReadedChecked = true;
            _isUnReadedChecked = true;

            RefreshGenresChecked();
            RefreshTagsChecked();
            RefreshProgressesChecked();

            ChangeMenuModeCommand = new RelayCommand(ChangeMenuMode);
            AddCategoryCommand = new RelayCommand(AddCategory);
            RemoveCategoryCommand = new RelayCommand(RemoveCategory);
            AddBookCommand = new RelayCommand(AddBook);
            AddBookByUrlCommand = new RelayCommand(AddBookByUrl);
            AddBookInCategoryCommand = new RelayCommand(AddBookInCategory);
            SaveTablesCommand = new RelayCommand(SaveTables);
            FilterCommand = new RelayCommand(Filter);
            SelectCommand = new RelayCommand(Select);
            AddSelectedToCategory = new RelayCommand(AddSelected);
            RemoveSelectedFromCategory = new RelayCommand(RemoveSelected);
            AddBookToPriorityCommand = new RelayCommand(AddBookToPriority);
            RemoveBookFromPriorityCommand = new RelayCommand(RemoveBookFromPriority);
            DeleteBookCommand = new RelayCommand(DeleteBook);
            CheckGenresCommand = new RelayCommand(CheckGenres);
            CheckTagsCommand = new RelayCommand(CheckTags);
            CheckProgressesCommand = new RelayCommand(CheckProgresses);
        }

        public RelayCommand ChangeMenuModeCommand { get; }
        public RelayCommand AddCategoryCommand { get; }
        public RelayCommand RemoveCategoryCommand { get; }
        public RelayCommand AddBookCommand { get; }
        public RelayCommand AddBookByUrlCommand { get; }
        public RelayCommand AddBookInCategoryCommand { get; }
        public RelayCommand SaveTablesCommand { get; }
        public RelayCommand FilterCommand { get; }
        public RelayCommand SelectCommand { get; }
        public RelayCommand AddSelectedToCategory { get; }
        public RelayCommand RemoveSelectedFromCategory { get; }
        public RelayCommand AddBookToPriorityCommand { get; }
        public RelayCommand RemoveBookFromPriorityCommand { get; }
        public RelayCommand DeleteBookCommand { get; }
        public RelayCommand CheckGenresCommand { get; }
        public RelayCommand CheckTagsCommand { get; }
        public RelayCommand CheckProgressesCommand { get; }

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

        public bool IsAllProgressesChecked
        {
            get => _isAllProgressesChecked;
            set { _isAllProgressesChecked = value; OnPropertyChanged(); }
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

        public void ChangeMenuMode(object? obj)
        {
            TablesViewModel.MenuMode = (BooksMenuMode)obj;
        }

        public void AddCategory(object? obj)
        {
            _model.AddCategory();
        }

        public void RemoveCategory(object? obj)
        {
            BookCategoryViewModel? categoryVM = obj as BookCategoryViewModel;

            if (categoryVM != null)
            {
                _model.RemoveCategory(categoryVM.Model);
            }
        }

        public void AddBook(object? obj)
        {
            _model.AddBook();
        }

        public void AddBookByUrl(object? obj)
        {
            _addEntityWindowService.OpenAddBookWindow();
        }

        public void AddBookInCategory(object? obj)
        {
            BookCategoryViewModel? categoryVM = obj as BookCategoryViewModel;

            if (categoryVM != null)
                _model.AddBook(categoryVM.Model);
        }

        public void SaveTables(object? obj)
        {
            _model.SaveTables();
        }

        public void Filter(object? obj)
        {
            RefreshGenresChecked();
            RefreshTagsChecked();
            RefreshProgressesChecked();

            var selectedGenres = TablesViewModel.GenreVMs.Where(x => x.IsChecked);
            var selectedTags = TablesViewModel.TagVMs.Where(x => x.IsChecked);
            var selectedProgress = TablesViewModel.ProgressVMs.Where(x => x.IsChecked);

            foreach (var item in TablesViewModel.BooksVMs)
            {
                item.IsFiltered = IsBookPassingFilter(selectedTags, selectedGenres, selectedProgress, item.Model);
            }

            foreach (var item in TablesViewModel.CategoryVMs)
            {
                item.IsFiltered = TablesViewModel.BooksVMs
                    .Where(x => x.Model.Category == item.Model)
                    .Any(x => x.IsFiltered);
            }
        }

        public void Select(object? obj)
        {
            BookViewModel? viewModel = obj as BookViewModel;
            SelectedBook = viewModel;
        }

        public void AddSelected(object? obj)
        {
            BookCategoryViewModel? categoryVM = obj as BookCategoryViewModel;

            if (categoryVM != null && SelectedBook != null)
            {
                _model.AddBookToCategory(categoryVM.Model, SelectedBook.Model);
            }
        }

        public void RemoveSelected(object? obj)
        {
            BookCategoryViewModel? categoryVM = obj as BookCategoryViewModel;

            if (categoryVM != null && SelectedBook != null)
            {
                _model.RemoveBookFromCategory(categoryVM.Model, SelectedBook.Model);
            }
        }

        public void AddBookToPriority(object? obj)
        {
            BookViewModel? viewModel = obj as BookViewModel;

            if (viewModel != null)
            {
                _model.AddBookToPriority(viewModel.Model);
            }
        }

        public void RemoveBookFromPriority(object? obj)
        {
            BookViewModel? viewModel = obj as BookViewModel;

            if (viewModel != null)
            {
                _model.RemoveBookFromPriority(viewModel.Model);
            }
        }

        public void DeleteBook(object? obj)
        {
            BookViewModel? viewModel = obj as BookViewModel;

            if (viewModel != null)
            {
                Book book = viewModel.Model;
                _model.DeleteBook(book);
            }
        }

        public void CheckGenres(object? obj)
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
        }

        public void CheckTags(object? obj)
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
        }

        public void CheckProgresses(object? obj)
        {
            if (IsAllProgressesChecked)
            {
                foreach (var vm in TablesViewModel.ProgressVMs)
                    vm.IsChecked = false;
            }
            else
            {
                foreach (var vm in TablesViewModel.ProgressVMs)
                    vm.IsChecked = true;
            }

            RefreshProgressesChecked();
            FilterCommand.Execute(obj);
        }

        private void RefreshGenresChecked()
        {
            IsAllGenresChecked = TablesViewModel.GenreVMs.All(x => x.IsChecked);
        }

        private void RefreshTagsChecked()
        {
            IsAllTagsChecked = TablesViewModel.TagVMs.All(x => x.IsChecked);
        }

        private void RefreshProgressesChecked()
        {
            IsAllProgressesChecked = TablesViewModel.ProgressVMs.All(x => x.IsChecked);
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

        private bool IsBookPassingFilter(IEnumerable<BookTagViewModel> tags, IEnumerable<BookGenreViewModel> genres, IEnumerable<BookReadProgressViewModel> progressCollection, Book book)
        {
            bool readedPassed = false;
            bool genresPassed = false;
            bool tagsPassed = false;

            readedPassed = progressCollection.Any(x => x.Model == book.ReadProgress);
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
