using Filmc.Entities.Entities;
using Filmc.Wpf.Commands;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Helper;
using Filmc.Wpf.Models;
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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Filmc.Wpf.ViewModels
{
    public class FilmsMenuViewModel : BaseViewModel
    {
        private readonly FilmsModel _model;
        private readonly AddEntityWindowService _addEntityWindowService;
        private readonly RecomendationMenuService _recomendationMenuService;

        private string _searchText;

        private bool _isWatchedChecked;
        private bool _isUnWatchedChecked;
        private bool _isAllGenresChecked;
        private bool _isAllTagsChecked;
        private bool _isAllProgressesChecked;

        private FilmViewModel? _selectedFilm;

        public FilmsMenuViewModel(FilmsModel model, UpdateMenuService updateMenuService, 
                                  BackgroundImageService backgroundImageService, AddEntityWindowService addEntityWindowService,
                                  RecomendationMenuService recomendationMenuService)
        {
            _model = model;
            _addEntityWindowService = addEntityWindowService;
            _recomendationMenuService = recomendationMenuService;

            BackgroundImageViewModel = new BackgroundImageViewModel(backgroundImageService);
            TablesViewModel = new FilmTablesViewModel(model, updateMenuService);

            _searchText = String.Empty;
            _isWatchedChecked = true;
            _isUnWatchedChecked = true;

            RefreshGenresChecked();
            RefreshTagsChecked();
            RefreshProgressesChecked();

            ChangeMenuModeCommand = new RelayCommand(ChangeMenuMode);
            OpenRecomentationsCommand = new RelayCommand(OpenRecomentations);
            AddCategoryCommand = new RelayCommand(AddCategory);
            RemoveCategoryCommand = new RelayCommand(RemoveCategory);
            AddFilmCommand = new RelayCommand(AddFilm);
            AddFilmByUrlCommand = new RelayCommand(AddFilmByUrl);
            AddFilmInCategoryCommand = new RelayCommand(AddFilmInCategory);
            SaveTablesCommand = new RelayCommand(SaveTables);
            FilterCommand = new RelayCommand(Filter);
            SelectCommand = new RelayCommand(Select);
            AddSelectedToCategory = new RelayCommand(AddSelected);
            RemoveSelectedFromCategory = new RelayCommand(RemoveSelected);
            AddFilmToPriorityCommand = new RelayCommand(AddFilmToPriority);
            RemoveFilmFromPriorityCommand = new RelayCommand(RemoveFilmFromPriority);
            DeleteFilmCommand = new RelayCommand(DeleteFilm);
            CheckGenresCommand = new RelayCommand(CheckGenres);
            CheckTagsCommand = new RelayCommand(CheckTags);
            CheckProgressesCommand = new RelayCommand(CheckProgresses);
            UpInCategoryCommand = new RelayCommand(UpInCategory);
            DownInCategoryCommand = new RelayCommand(DownInCategory);
            RemoveFromCategoryCommand = new RelayCommand(RemoveFromCategory);
        }

        public RelayCommand ChangeMenuModeCommand { get; }
        public RelayCommand OpenRecomentationsCommand { get; }
        public RelayCommand AddCategoryCommand { get; }
        public RelayCommand RemoveCategoryCommand { get; }
        public RelayCommand AddFilmCommand { get; }
        public RelayCommand AddFilmByUrlCommand { get; }
        public RelayCommand AddFilmInCategoryCommand { get; }
        public RelayCommand SaveTablesCommand { get; }
        public RelayCommand FilterCommand { get; }
        public RelayCommand SelectCommand { get; }
        public RelayCommand AddSelectedToCategory { get; }
        public RelayCommand RemoveSelectedFromCategory { get; }
        public RelayCommand AddFilmToPriorityCommand { get; }
        public RelayCommand RemoveFilmFromPriorityCommand { get; }
        public RelayCommand DeleteFilmCommand { get; }
        public RelayCommand CheckGenresCommand { get; }
        public RelayCommand CheckTagsCommand { get; }
        public RelayCommand CheckProgressesCommand { get; }
        public RelayCommand UpInCategoryCommand { get; }
        public RelayCommand DownInCategoryCommand { get; }
        public RelayCommand RemoveFromCategoryCommand { get; }

        public FilmTablesViewModel TablesViewModel { get; }
        public BackgroundImageViewModel BackgroundImageViewModel { get; }

        public string SearchText
        {
            get => _searchText;
            set
            { 
                _searchText = value;

                string search = value.ToLowerInvariant();

                SearchInFilms(search);
                SearchInCategories(search);

                OnPropertyChanged(); 
            }
        }

        public bool IsWatchedChecked
        {
            get => _isWatchedChecked;
            set
            {
                if (IsUnWatchedChecked != false || value != false)
                {
                    _isWatchedChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsUnWatchedChecked
        {
            get => _isUnWatchedChecked;
            set
            {
                if (IsWatchedChecked != false || value != false)
                {
                    _isUnWatchedChecked = value;
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

        public FilmViewModel? SelectedFilm
        {
            get => _selectedFilm;
            set
            {
                if (_selectedFilm != null)
                    _selectedFilm.IsSelected = false;

                _selectedFilm = value;

                if (_selectedFilm != null)
                    _selectedFilm.IsSelected = true;

                OnPropertyChanged();
            }
        }

        public void ChangeMenuMode(object? obj)
        {
            TablesViewModel.MenuMode = (FilmsMenuMode)obj;
        }

        public void OpenRecomentations(object? obj)
        {
            _recomendationMenuService.OpenRecomendations(TablesViewModel);
        }

        public void AddCategory(object? obj)
        {
            _model.AddCategory();
        }

        public void RemoveCategory(object? obj)
        {
            FilmCategoryViewModel? categoryVM = obj as FilmCategoryViewModel;

            if (categoryVM != null)
            {
                _model.RemoveCategory(categoryVM.Model);
            }
        }

        public void AddFilm(object? obj)
        {
            _model.AddFilm();
        }

        public void AddFilmByUrl(object? obj)
        {
            _addEntityWindowService.OpenAddFilmWindow();
        }

        public void AddFilmInCategory(object? obj)
        {
            FilmCategoryViewModel? categoryVM = obj as FilmCategoryViewModel;

            if (categoryVM != null)
                _model.AddFilm(categoryVM.Model);
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

            foreach (var item in TablesViewModel.FilmVMs)
            {
                item.IsFiltered = IsFilmPassingFilter(selectedTags, selectedGenres, selectedProgress, item.Model);
            }

            foreach (var item in TablesViewModel.CategoryVMs)
            {
                item.IsFiltered = TablesViewModel.FilmVMs
                    .Where(x => x.Model.Category == item.Model)
                    .Any(x => x.IsFiltered);
            }
        }

        public void Select(object? obj)
        {
            FilmViewModel? viewModel = obj as FilmViewModel;
            SelectedFilm = viewModel;
        }

        public void AddSelected(object? obj)
        {
            FilmCategoryViewModel? categoryVM = obj as FilmCategoryViewModel;

            if (categoryVM != null && SelectedFilm != null)
            {
                _model.AddFilmToCategory(categoryVM.Model, SelectedFilm.Model);
            }
        }

        public void RemoveSelected(object? obj)
        {
            FilmCategoryViewModel? categoryVM = obj as FilmCategoryViewModel;

            if (categoryVM != null && SelectedFilm != null)
            {
                _model.RemoveFilmFromCategory(categoryVM.Model, SelectedFilm.Model);
            }
        }

        public void AddFilmToPriority(object? obj)
        {
            FilmViewModel? viewModel = obj as FilmViewModel;

            if (viewModel != null)
            {
                _model.AddFilmToPriority(viewModel.Model);
            }
        }

        public void RemoveFilmFromPriority(object? obj)
        {
            FilmViewModel? viewModel = obj as FilmViewModel;

            if (viewModel != null)
            {
                _model.RemoveFilmFromPriority(viewModel.Model);
            }
        }

        public void DeleteFilm(object? obj)
        {
            FilmViewModel? viewModel = obj as FilmViewModel;

            if (viewModel != null)
            {
                Film film = viewModel.Model;
                _model.DeleteFilm(film);
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

        private void SearchInFilms(string search)
        {
            foreach (var viewModel in TablesViewModel.FilmVMs)
                viewModel.IsFinded = false;

            var filtredVMs = TablesViewModel.FilmVMs
                .Where(x => x.Name.SearchBy(search));

            foreach (var viewModel in filtredVMs)
                viewModel.IsFinded = true;
        }

        private void SearchInCategories(string search)
        {
            foreach (var viewModel in TablesViewModel.CategoryVMs)
                viewModel.IsFinded = false;

            var filtredVMs = TablesViewModel.CategoryVMs
                .Where(x => x.Model.Name.SearchBy(search) || x.Model.Films.Any(y => y.Name.SearchBy(search)));

            foreach (var viewModel in filtredVMs)
                viewModel.IsFinded = true;
        }

        private bool IsFilmPassingFilter(IEnumerable<FilmTagViewModel> tags, IEnumerable<FilmGenreViewModel> genres, IEnumerable<FilmWatchProgressViewModel> progressCollection, Film film)
        {
            bool watchedPassed = false;
            bool genresPassed = false;
            bool tagsPassed = false;

            watchedPassed = progressCollection.Any(x => x.Model == film.WatchProgress);
            genresPassed = genres.Any(x => x.Model == film.Genre);

            if (tags.Count() != TablesViewModel.TagVMs.Count)
            {
                var ft = tags.Select(i => i.Model);
                tagsPassed = film.Tags.IntersectBy(ft, x => x).Any();
            }
            else
            {
                tagsPassed = true;
            }

            return watchedPassed && genresPassed && tagsPassed;
        }

        public void UpInCategory(object? obj)
        {
            FilmViewModel film = (FilmViewModel)obj!;

            var category = TablesViewModel.CategoryVMs.First(x => x.Id == film.CategoryId);
            category.UpInCategory(film);
        }

        public void DownInCategory(object? obj)
        {
            FilmViewModel film = (FilmViewModel)obj!;

            var category = TablesViewModel.CategoryVMs.First(x => x.Id == film.CategoryId);
            category.DownInCategory(film);
        }

        public void RemoveFromCategory(object? obj)
        {
            FilmViewModel film = (FilmViewModel)obj!;

            var category = TablesViewModel.CategoryVMs.First(x => x.Id == film.CategoryId);
            category.RemoveFromCategory(film);
        }
    }
}
