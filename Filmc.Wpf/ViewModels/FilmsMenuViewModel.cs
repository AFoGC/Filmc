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
using System.Windows.Data;

namespace Filmc.Wpf.ViewModels
{
    public class FilmsMenuViewModel : BaseViewModel
    {
        private readonly FilmsModel _model;

        private string _searchText;
        private bool _isWatchedChecked;
        private bool _isUnWatchedChecked;

        private RelayCommand? changeMenuModeCommand;
        private RelayCommand? addCategoryCommand;
        private RelayCommand? addBookCommand;
        private RelayCommand? saveTablesCommand;
        private RelayCommand? filterCommand;
        private RelayCommand? sortTable;

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
            set { _isWatchedChecked = value; OnPropertyChanged(); }
        }

        public bool IsUnWatchedChecked
        {
            get => _isUnWatchedChecked;
            set { _isUnWatchedChecked = value; OnPropertyChanged(); }
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
            

        public RelayCommand AddFilmCommand
        {
            get
            {
                return addBookCommand ?? 
                (addBookCommand = new RelayCommand(obj =>
                {
                    _model.AddFilm();
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
                        if ()
                        {

                        }
                    }
                }));
            }
        }

        private void FilterFilm(IEnumerable<FilmGenreViewModel> genres, Film film)
        {
            genres.Any(x => x.Model == film.Genre) && film.IsWatched == 
        }


        public RelayCommand SortTable
        {
            get
            {
                return sortTable ?? (sortTable = new RelayCommand(obj =>
                {
                    string str = obj as string;

                    switch (TablesViewModel.MenuMode)
                    {
                        case FilmsMenuMode.Categories:
                            TablesViewModel.CategoriesVC.ChangeSortProperty(str);
                            TablesViewModel.FilmsSimplifiedVC.ChangeSortProperty(str);
                            break;

                        case FilmsMenuMode.Films:
                            TablesViewModel.FilmsVC.ChangeSortProperty(str);
                            break;

                        case FilmsMenuMode.Series:
                            TablesViewModel.SeriesVC.ChangeSortProperty(str);
                            break;

                        case FilmsMenuMode.Priorities:
                            TablesViewModel.PrioritiesVC.ChangeSortProperty(str);
                            break;
                    }
                }));
            }
        }
    }
}
