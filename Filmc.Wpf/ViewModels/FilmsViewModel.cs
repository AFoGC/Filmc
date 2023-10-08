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
    public class FilmsViewModel
    {
        private readonly FilmsModel _model;

        private TablesContext? _tablesContext;

        public FilmsViewModel(FilmsModel model)
        {
            FilmVMs = new ObservableCollection<FilmViewModel>();
            CategoryVMs = new ObservableCollection<FilmCategoryViewModel>();
            GenreVMs = new ObservableCollection<FilmGenreViewModel>();

            _model = model;
            _model.TablesContextChanged += OnTablesContextChanged;
            OnTablesContextChanged();

            CategoriesVC = new FilmCategoriesViewCollection(CategoryVMs);
            FilmsSimplifiedVC = new FilmsSimplifiedViewCollection(FilmVMs);
            FilmsVC = new FilmsViewCollection(FilmVMs);
            SeriesVC = new FilmSeriesViewCollection(FilmVMs);
        }

        public ObservableCollection<FilmViewModel> FilmVMs { get; }
        public ObservableCollection<FilmCategoryViewModel> CategoryVMs { get; }
        public ObservableCollection<FilmGenreViewModel> GenreVMs { get; }

        public FilmCategoriesViewCollection CategoriesVC { get; }
        public FilmsSimplifiedViewCollection FilmsSimplifiedVC { get; }
        public FilmsViewCollection FilmsVC { get; }
        public FilmSeriesViewCollection SeriesVC { get; }

        private void OnTablesContextChanged()
        {
            if (_tablesContext != null)
            {
                _tablesContext.Films.CollectionChanged -= OnFilmsChanged;
                _tablesContext.FilmGenres.CollectionChanged -= OnGenresCollectionChanged;
                _tablesContext.FilmCategories.CollectionChanged -= OnCategoriesCollectionChanged;
            }

            _tablesContext = _model.TablesContext;

            _tablesContext.Films.CollectionChanged += OnFilmsChanged;
            _tablesContext.FilmGenres.CollectionChanged += OnGenresCollectionChanged;
            _tablesContext.FilmCategories.CollectionChanged += OnCategoriesCollectionChanged;
        }

        private void OnFilmsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                Film film = (Film)e.NewItems[0]!;
                FilmVMs.Insert(e.NewStartingIndex, new FilmViewModel(film));
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
                CategoryVMs.Insert(e.NewStartingIndex, new FilmCategoryViewModel(entity));
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
                GenreVMs.Insert(e.NewStartingIndex, new FilmGenreViewModel(entity));
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
    }
}
