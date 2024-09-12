using Filmc.Entities.Entities;
using Filmc.Wpf.Commands;
using Filmc.Wpf.EntityViewModels;
using Filmc.Wpf.Models;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.Services;
using Filmc.Wpf.ViewCollections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.ViewModels
{
    public class FilmTablesViewModel : BaseViewModel
    {
        private readonly FilmsModel _model;

        private readonly UpdateMenuService _updateMenuService;

        private readonly EntityObserver<Film, FilmViewModel> _filmEntityObserver;
        private readonly EntityObserver<FilmCategory, FilmCategoryViewModel> _categoryEntityObserver;
        private readonly EntityObserver<FilmGenre, FilmGenreViewModel> _genreEntityObserver;
        private readonly EntityObserver<FilmTag, FilmTagViewModel> _tagEntityObserver;
        private readonly EntityObserver<FilmWatchProgress, FilmWatchProgressViewModel> _progressEntityObserver;

        private RepositoriesFacade? _tablesContext;
        private FilmsMenuMode _menuMode;

        private RelayCommand? sortTable;

        public FilmTablesViewModel(FilmsModel model, UpdateMenuService updateMenuService)
        {
            _menuMode = FilmsMenuMode.Categories;

            FilmVMs = new ObservableCollection<FilmViewModel>();
            CategoryVMs = new ObservableCollection<FilmCategoryViewModel>();
            GenreVMs = new ObservableCollection<FilmGenreViewModel>();
            TagVMs = new ObservableCollection<FilmTagViewModel>();
            ProgressVMs = new ObservableCollection<FilmWatchProgressViewModel>();

            _filmEntityObserver = new EntityObserver<Film, FilmViewModel>(FilmVMs, CreateFilmViewModel);
            _categoryEntityObserver = new EntityObserver<FilmCategory, FilmCategoryViewModel>(CategoryVMs, CreateCategoryViewModel);
            _genreEntityObserver = new EntityObserver<FilmGenre, FilmGenreViewModel>(GenreVMs, CreateGenreViewModel);
            _tagEntityObserver = new EntityObserver<FilmTag, FilmTagViewModel>(TagVMs, CreateTagViewModel);
            _progressEntityObserver = new EntityObserver<FilmWatchProgress, FilmWatchProgressViewModel>(ProgressVMs, CreateProgressViewModel);

            _model = model;
            _updateMenuService = updateMenuService;
            _model.TablesContextChanged += OnTablesContextChanged;
            OnTablesContextChanged();

            CategoriesVC = new FilmCategoriesViewCollection(CategoryVMs);
            FilmsSimplifiedVC = new FilmsSimplifiedViewCollection(FilmVMs);
            FilmsVC = new FilmsViewCollection(FilmVMs);
            SeriesVC = new FilmSeriesViewCollection(FilmVMs);
            PrioritiesVC = new FilmsInPriorityViewCollection(FilmVMs);

            CategoriesVC.ChangeSortProperty("Id");
            FilmsSimplifiedVC.ChangeSortProperty("Id");
            FilmsVC.ChangeSortProperty("Id");
            SeriesVC.ChangeSortProperty("Id");

            SortTable = new RelayCommand(Sort);
        }

        public RelayCommand SortTable { get; }

        public ObservableCollection<FilmViewModel> FilmVMs { get; }
        public ObservableCollection<FilmCategoryViewModel> CategoryVMs { get; }
        public ObservableCollection<FilmGenreViewModel> GenreVMs { get; }
        public ObservableCollection<FilmTagViewModel> TagVMs { get; }
        public ObservableCollection<FilmWatchProgressViewModel> ProgressVMs { get; }

        public FilmCategoriesViewCollection CategoriesVC { get; }
        public FilmsSimplifiedViewCollection FilmsSimplifiedVC { get; }
        public FilmsViewCollection FilmsVC { get; }
        public FilmSeriesViewCollection SeriesVC { get; }
        public FilmsInPriorityViewCollection PrioritiesVC { get; }

        public FilmsMenuMode MenuMode
        {
            get => _menuMode;
            set { _menuMode = value; OnPropertyChanged(); }
        }

        private void OnTablesContextChanged()
        {
            _tablesContext = _model.TablesContext;

            _filmEntityObserver.SetSource(_tablesContext.Films);
            _categoryEntityObserver.SetSource(_tablesContext.FilmCategories);
            _genreEntityObserver.SetSource(_tablesContext.FilmGenres);
            _tagEntityObserver.SetSource(_tablesContext.FilmTags);
            _progressEntityObserver.SetSource(_tablesContext.FilmProgresses);
        }

        public void Sort(object? obj)
        {
            string? str = obj as string;

            switch (MenuMode)
            {
                case FilmsMenuMode.Categories:
                    CategoriesVC.ChangeSortProperty(str);
                    FilmsSimplifiedVC.ChangeSortProperty(str);
                    break;

                case FilmsMenuMode.Films:
                    FilmsVC.ChangeSortProperty(str);
                    break;

                case FilmsMenuMode.Series:
                    SeriesVC.ChangeSortProperty(str);
                    break;

                case FilmsMenuMode.Priorities:
                    PrioritiesVC.ChangeSortProperty(str);
                    break;
            }
        }

        private FilmViewModel CreateFilmViewModel(Film film)
        {
            return new FilmViewModel(film, _updateMenuService);
        }

        private FilmCategoryViewModel CreateCategoryViewModel(FilmCategory filmCategory)
        {
            return new FilmCategoryViewModel(filmCategory, FilmVMs, _updateMenuService, _tablesContext);
        }

        private FilmGenreViewModel CreateGenreViewModel(FilmGenre filmGenre)
        {
            return new FilmGenreViewModel(filmGenre);
        }

        private FilmTagViewModel CreateTagViewModel(FilmTag filmTag)
        {
            return new FilmTagViewModel(filmTag);
        }

        private FilmWatchProgressViewModel CreateProgressViewModel(FilmWatchProgress watchProgress)
        {
            return new FilmWatchProgressViewModel(watchProgress);
        }
    }
}
