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
        }

        public ObservableCollection<FilmViewModel> FilmVMs { get; }
        public ObservableCollection<FilmCategoryViewModel> CategoryVMs { get; }
        public ObservableCollection<FilmGenreViewModel> GenreVMs { get; }
        public ObservableCollection<FilmTagViewModel> TagVMs { get; }

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
            if (_tablesContext != null)
            {
                _tablesContext.Films.CollectionChanged -= OnFilmsChanged;
                _tablesContext.FilmGenres.CollectionChanged -= OnGenresCollectionChanged;
                _tablesContext.FilmCategories.CollectionChanged -= OnCategoriesCollectionChanged;
                _tablesContext.FilmTags.CollectionChanged -= OnTagsCollectionChanged;
            }

            _tablesContext = _model.TablesContext;

            FilmVMs.Clear();
            CategoryVMs.Clear();
            GenreVMs.Clear();
            TagVMs.Clear();

            foreach (var item in _tablesContext.Films)
                FilmVMs.Add(new FilmViewModel(item, _updateMenuService));

            foreach (var item in _tablesContext.FilmCategories)
                CategoryVMs.Add(new FilmCategoryViewModel(item, FilmVMs, _updateMenuService, _tablesContext));

            foreach (var item in _tablesContext.FilmGenres)
                GenreVMs.Add(new FilmGenreViewModel(item));

            foreach (var item in _tablesContext.FilmTags)
                TagVMs.Add(new FilmTagViewModel(item));

            _tablesContext.Films.CollectionChanged += OnFilmsChanged;
            _tablesContext.FilmGenres.CollectionChanged += OnGenresCollectionChanged;
            _tablesContext.FilmCategories.CollectionChanged += OnCategoriesCollectionChanged;
            _tablesContext.FilmTags.CollectionChanged += OnTagsCollectionChanged;
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
                }));
            }
        }

        private void OnFilmsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Film film = (Film)e.NewItems[0]!;
                FilmVMs.Insert(e.NewStartingIndex, new FilmViewModel(film, _updateMenuService));
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                int i = e.OldStartingIndex;
                FilmVMs.RemoveAt(i);
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                FilmVMs.Clear();
            }
        }

        private void OnCategoriesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                FilmCategory entity = (FilmCategory)e.NewItems[0]!;
                CategoryVMs.Insert(e.NewStartingIndex, new FilmCategoryViewModel(entity, FilmVMs, _updateMenuService, _tablesContext));
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                int i = e.OldStartingIndex;
                CategoryVMs.RemoveAt(i);
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                CategoryVMs.Clear();
            }
        }

        private void OnGenresCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                FilmGenre entity = (FilmGenre)e.NewItems[0]!;
                GenreVMs.Insert(e.NewStartingIndex, new FilmGenreViewModel(entity)); ;
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                int i = e.OldStartingIndex;
                GenreVMs.RemoveAt(i);
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                GenreVMs.Clear();
            }
        }

        private void OnTagsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                FilmTag entity = (FilmTag)e.NewItems[0]!;
                TagVMs.Insert(e.NewStartingIndex, new FilmTagViewModel(entity));
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                int i = e.OldStartingIndex;
                TagVMs.RemoveAt(i);
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                TagVMs.Clear();
            }
        }
    }
}
