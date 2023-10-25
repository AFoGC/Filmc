using Filmc.Wpf.Commands;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Models;
using Filmc.Wpf.ViewCollections;
using Filmc.Xtl;
using Filmc.Xtl.Entities;
using Filmc.Xtl.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Filmc.Wpf.ViewModels
{
    public class FilmsMenuViewModel : BaseViewModel
    {
        private readonly FilmsModel _model;

        private string _searchText;
        private bool _isWatchedChecked;
        private bool _isUnWatchedChecked;
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

        public FilmsMenuViewModel(FilmsModel model)
        {
            _model = model;
            TablesViewModel = new FilmTablesViewModel(model);

            _searchText = String.Empty;
            _isWatchedChecked = true;
            _isUnWatchedChecked = true;
        }

        public FilmTablesViewModel TablesViewModel { get; }

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value;  OnPropertyChanged(); }
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
                        _model.AddFilm(categoryVM.Model.Id);
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

                    foreach (var item in TablesViewModel.FilmVMs)
                    {
                        item.IsFiltered = IsFilmPassingFilter(selectedGenres, item.Model);
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
                        categoryVM.Model.AddFilmInOrder(SelectedFilm.Model);
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
                        categoryVM.Model.RemoveFilmInOrder(SelectedFilm.Model);
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
                        if (_model.TablesContext.FilmInPriorities.All(x => x.Id != viewModel.Model.Id))
                        {
                            FilmInPriority priority = new FilmInPriority() { Id = viewModel.Model.Id, CreationTime = DateTime.Now };
                            _model.TablesContext.FilmInPriorities.Add(priority);
                        }
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
                        _model.TablesContext.FilmInPriorities.Remove(viewModel.Model.Priority);
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

                        if (film.CategoryId != 0)
                            film.CategoryId = 0;

                        if (film.Priority != null)
                            _model.TablesContext.FilmInPriorities.Remove(film.Priority);

                        _model.TablesContext.Films.Remove(film);
                    }
                }));
            }
        }

        private bool IsFilmPassingFilter(IEnumerable<FilmGenreViewModel> genres, Film film)
        {
            bool exp = false;

            if (genres.Any(x => x.Model == film.Genre))
                exp = film.IsWatched == IsWatchedChecked || film.IsWatched != IsUnWatchedChecked;

            return exp;
        }
    }
}
