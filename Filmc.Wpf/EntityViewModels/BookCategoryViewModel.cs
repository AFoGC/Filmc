using Filmc.Entities.Entities;
using Filmc.Wpf.Commands;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.Services;
using Filmc.Wpf.ViewCollections;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Filmc.Wpf.EntityViewModels
{
    public class BookCategoryViewModel : BaseEntityViewModel
    {
        public BookCategory Model { get; }

        private readonly UpdateMenuService _updateMenuService;
        private readonly IRepositoriesSaved _repositories;

        private bool _isCollectionVisible;
        private bool _isSelected;

        public BookCategoryViewModel(BookCategory model, ObservableCollection<BookViewModel> bookViewModels, 
                                     UpdateMenuService updateMenuService, IRepositoriesSaved repositories)
        {
            Model = model;
            Model.PropertyChanged += OnModelPropertyChanged;
            Model.Mark.PropertyChanged += OnModelPropertyChanged;

            _updateMenuService = updateMenuService;
            _repositories = repositories;

            _isCollectionVisible = true;
            BooksVC = new BooksInCategoryViewCollection(model, bookViewModels);

            CollapseCommand = new RelayCommand(Collapse);
            OpenedContextMenuCommand = new RelayCommand(OpenedContextMenu);
            ClosedContextMenuCommand = new RelayCommand(ClosedContextMenu);
            UpInCategoryCommand = new RelayCommand(UpInCategory);
            DownInCategoryCommand = new RelayCommand(DownInCategory);
            RemoveFromCategoryCommand = new RelayCommand(RemoveFromCategory);
            OpenUpdateMenuCommand = new RelayCommand(OpenUpdateMenu);
            RemoveMarkCommand = new RelayCommand(RemoveMark);
        }

        public RelayCommand CollapseCommand { get; }
        public RelayCommand OpenedContextMenuCommand { get; }
        public RelayCommand ClosedContextMenuCommand { get; }
        public RelayCommand UpInCategoryCommand { get; }
        public RelayCommand DownInCategoryCommand { get; }
        public RelayCommand RemoveFromCategoryCommand { get; }
        public RelayCommand OpenUpdateMenuCommand { get; }
        public RelayCommand RemoveMarkCommand { get; }

        public BooksInCategoryViewCollection BooksVC { get; }

        public bool IsCollectionVisible
        {
            get => _isCollectionVisible;
            set
            {
                _isCollectionVisible = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get => Model.Id;
            set => Model.Id = value;
        }
        public string Name
        {
            get => Model.Name;
            set => Model.Name = value;
        }
        public string HideName
        {
            get => Model.HideName;
            set => Model.HideName = value;
        }

        public int? FormatedMark
        {
            get => Model.Mark.FormatedMark;
            set => Model.Mark.FormatedMark = value;
        }
        public int MarkSystem
        {
            get => Model.Mark.MarkSystem;
            set => Model.Mark.MarkSystem = value;
        }
        public int? RawMark
        {
            get => Model.Mark.RawMark;
        }

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }

        public void Collapse(object? obj)
        {
            IsCollectionVisible = !IsCollectionVisible;
        }

        public void OpenedContextMenu(object? obj)
        {
            IsSelected = true;
        }

        public void ClosedContextMenu(object? obj)
        {
            IsSelected = false;
        }

        public void UpInCategory(object? obj)
        {
            BookViewModel? bookViewModel = obj as BookViewModel;

            if (bookViewModel != null)
            {
                Model.ChangeCategoryListId(bookViewModel.Model, bookViewModel.Model.CategoryListId - 1);
                _repositories.SaveChanges();
            }
        }

        public void DownInCategory(object? obj)
        {
            BookViewModel? bookViewModel = obj as BookViewModel;

            if (bookViewModel != null)
            {
                Model.ChangeCategoryListId(bookViewModel.Model, bookViewModel.Model.CategoryListId + 1);
                _repositories.SaveChanges();
            }
        }

        public void RemoveFromCategory(object? obj)
        {
            BookViewModel? bookViewModel = obj as BookViewModel;

            if (bookViewModel != null)
            {
                Model.RemoveBookInOrder(bookViewModel.Model);
                _repositories.SaveChanges();
            }
        }

        public void OpenUpdateMenu(object? obj)
        {
            _updateMenuService.OpenUpdateMenu(this);
        }

        public void RemoveMark(object? obj)
        {
            Model.Mark.RawMark = null;
        }

        private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
