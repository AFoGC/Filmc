﻿using Filmc.Wpf.Commands;
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
            if (_tablesContext != null)
            {
                _tablesContext.Books.CollectionChanged -= OnBooksChanged;
                _tablesContext.BookGenres.CollectionChanged -= OnGenresCollectionChanged;
                _tablesContext.BookCategories.CollectionChanged -= OnCategoriesCollectionChanged;
                _tablesContext.BookTags.CollectionChanged -= OnTagsCollectionChanged;
            }

            _tablesContext = _model.TablesContext;

            BooksVMs.Clear();
            CategoryVMs.Clear();
            GenreVMs.Clear();
            TagVMs.Clear();

            foreach (var item in _tablesContext.Books)
                BooksVMs.Add(new BookViewModel(item, _updateMenuService));

            foreach (var item in _tablesContext.BookCategories)
                CategoryVMs.Add(new BookCategoryViewModel(item, BooksVMs, _updateMenuService, _tablesContext));

            foreach (var item in _tablesContext.BookGenres)
                GenreVMs.Add(new BookGenreViewModel(item));

            foreach (var item in _tablesContext.BookTags)
                TagVMs.Add(new BookTagViewModel(item));

            _tablesContext.Books.CollectionChanged += OnBooksChanged;
            _tablesContext.BookGenres.CollectionChanged += OnGenresCollectionChanged;
            _tablesContext.BookCategories.CollectionChanged += OnCategoriesCollectionChanged;
            _tablesContext.BookTags.CollectionChanged += OnTagsCollectionChanged;
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

        private void OnBooksChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Book book = (Book)e.NewItems[0]!;
                BooksVMs.Insert(e.NewStartingIndex, new BookViewModel(book, _updateMenuService));
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                int i = e.OldStartingIndex;
                BooksVMs.RemoveAt(i);
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                BooksVMs.Clear();
            }
        }

        private void OnCategoriesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                BookCategory entity = (BookCategory)e.NewItems[0]!;
                CategoryVMs.Insert(e.NewStartingIndex, new BookCategoryViewModel(entity, BooksVMs, _updateMenuService, _tablesContext));
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
                BookGenre entity = (BookGenre)e.NewItems[0]!;
                GenreVMs.Insert(e.NewStartingIndex, new BookGenreViewModel(entity)); ;
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
                BookTag entity = (BookTag)e.NewItems[0]!;
                TagVMs.Insert(e.NewStartingIndex, new BookTagViewModel(entity));
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
