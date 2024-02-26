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

        private string _searchText;

        private bool _isWatchedChecked;
        private bool _isUnWatchedChecked;
        private bool _isAllGenresChecked;
        private bool _isAllTagsChecked;

        private FilmViewModel? _selectedFilm;

        private RelayCommand? changeMenuModeCommand;
        private RelayCommand? addCategoryCommand;
        private RelayCommand? addFilmCommand;
        private RelayCommand? addFilmInCategoryCommand;
        private RelayCommand? saveTablesCommand;
        private RelayCommand? filterCommand;
        private RelayCommand? selectCommand;
        private RelayCommand? addSelectedToCategory;
        private RelayCommand? removeSelectedFromCategory;
        private RelayCommand? removeCategoryCommand;
        private RelayCommand? addFilmToPriorityCommand;
        private RelayCommand? deleteFilmCommand;
        private RelayCommand? removeFilmFromPriorityCommand;
        private RelayCommand? checkGenresCommand;
        private RelayCommand? checkTagsCommand;

        public FilmsMenuViewModel(FilmsModel model, UpdateMenuService updateMenuService, BackgroundImageService backgroundImageService)
        {
            _model = model;
            BackgroundImageViewModel = new BackgroundImageViewModel(backgroundImageService);
            TablesViewModel = new FilmTablesViewModel(model, updateMenuService);

            _searchText = String.Empty;
            _isWatchedChecked = true;
            _isUnWatchedChecked = true;

            RefreshGenresChecked();
            RefreshTagsChecked();
        }

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

        public RelayCommand ChangeMenuModeCommand
        {
            get
            {
                return changeMenuModeCommand ??
                (changeMenuModeCommand = new RelayCommand(obj =>
                {
                    TablesViewModel.MenuMode = (FilmsMenuMode)obj;
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
                    FilmCategoryViewModel? categoryVM = obj as FilmCategoryViewModel;

                    if (categoryVM != null)
                    {
                        _model.RemoveCategory(categoryVM.Model);
                    }
                }));
            }
        }


        public RelayCommand AddFilmCommand
        {
            get
            {
                return addFilmCommand ?? 
                (addFilmCommand = new RelayCommand(obj =>
                {
                    _model.AddFilm();
                }));
            }
        }

        public RelayCommand AddFilmInCategoryCommand
        {
            get
            {
                return addFilmInCategoryCommand ??
                (addFilmInCategoryCommand = new RelayCommand(obj =>
                {
                    FilmCategoryViewModel? categoryVM = obj as FilmCategoryViewModel;

                    if (categoryVM != null)
                        _model.AddFilm(categoryVM.Model);
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

                    foreach (var item in TablesViewModel.FilmVMs)
                    {
                        item.IsFiltered = IsFilmPassingFilter(selectedTags, selectedGenres, item.Model);
                    }

                    foreach (var item in TablesViewModel.CategoryVMs)
                    {
                        item.IsFiltered = TablesViewModel.FilmVMs
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
                    FilmViewModel? viewModel = obj as FilmViewModel;
                    SelectedFilm = viewModel;
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
                    FilmCategoryViewModel? categoryVM = obj as FilmCategoryViewModel;

                    if (categoryVM != null && SelectedFilm != null)
                    {
                        _model.AddFilmToCategory(categoryVM.Model, SelectedFilm.Model);
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
                    FilmCategoryViewModel? categoryVM = obj as FilmCategoryViewModel;

                    if (categoryVM != null && SelectedFilm != null)
                    {
                        _model.RemoveFilmFromCategory(categoryVM.Model, SelectedFilm.Model);
                    }
                }));
            }
        }

        public RelayCommand AddFilmToPriorityCommand
        {
            get
            {
                return addFilmToPriorityCommand ??
                (addFilmToPriorityCommand = new RelayCommand(obj =>
                {
                    FilmViewModel? viewModel = obj as FilmViewModel;

                    if (viewModel != null)
                    {
                        _model.AddFilmToPriority(viewModel.Model);
                    }
                }));
            }
        }

        public RelayCommand RemoveFilmFromPriorityCommand
        {
            get
            {
                return removeFilmFromPriorityCommand ??
                (removeFilmFromPriorityCommand = new RelayCommand(obj =>
                {
                    FilmViewModel? viewModel = obj as FilmViewModel;

                    if (viewModel != null)
                    {
                        _model.RemoveFilmFromPriority(viewModel.Model);
                    }
                }));
            }
        }

        public RelayCommand DeleteFilmCommand
        {
            get
            {
                return deleteFilmCommand ??
                (deleteFilmCommand = new RelayCommand(obj =>
                {
                    FilmViewModel? viewModel = obj as FilmViewModel;

                    if (viewModel != null)
                    {
                        Film film = viewModel.Model;
                        _model.DeleteFilm(film);
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

        private bool IsFilmPassingFilter(IEnumerable<FilmTagViewModel> tags, IEnumerable<FilmGenreViewModel> genres, Film film)
        {
            bool watchedPassed = false;
            bool genresPassed = false;
            bool tagsPassed = false;

            watchedPassed = film.IsWatched == IsWatchedChecked || film.IsWatched != IsUnWatchedChecked;
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
    }
}
