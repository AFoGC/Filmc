using Filmc.Entities.Entities;
using Filmc.Wpf.Commands;
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

        private bool _isCollectionVisible;
        private bool _isSelected;

        private RelayCommand? collapseCommand;
        private RelayCommand? openedContextMenuCommand;
        private RelayCommand? closedContextMenuCommand;
        private RelayCommand? upInCategoryCommand;
        private RelayCommand? downInCategoryCommand;
        private RelayCommand? removeFromCategoryCommand;
        private RelayCommand? openUpdateMenuCommand;
        private RelayCommand? removeMarkCommand;

        public BookCategoryViewModel(BookCategory model, ObservableCollection<BookViewModel> bookViewModels, UpdateMenuService updateMenuService)
        {
            Model = model;
            Model.PropertyChanged += OnModelPropertyChanged;
            Model.Mark.PropertyChanged += OnModelPropertyChanged;

            _updateMenuService = updateMenuService;

            _isCollectionVisible = true;
            BooksVC = new BooksInCategoryViewCollection(model, bookViewModels);
        }

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

        public RelayCommand CollapseCommand
        {
            get
            {
                return collapseCommand ??
                (collapseCommand = new RelayCommand(obj =>
                {
                    IsCollectionVisible = !IsCollectionVisible;
                }));
            }
        }

        public RelayCommand OpenedContextMenuCommand
        {
            get
            {
                return openedContextMenuCommand ??
                (openedContextMenuCommand = new RelayCommand(obj =>
                {
                    IsSelected = true;
                }));
            }
        }

        public RelayCommand ClosedContextMenuCommand
        {
            get
            {
                return closedContextMenuCommand ??
                (closedContextMenuCommand = new RelayCommand(obj =>
                {
                    IsSelected = false;
                }));
            }
        }

        public RelayCommand UpInCategoryCommand
        {
            get
            {
                return upInCategoryCommand ??
                (upInCategoryCommand = new RelayCommand(obj =>
                {
                    BookViewModel? bookViewModel = obj as BookViewModel;

                    if (bookViewModel != null)
                    {
                        Model.ChangeCategoryListId(bookViewModel.Model, bookViewModel.Model.CategoryListId - 1);
                    }
                }));
            }
        }

        public RelayCommand DownInCategoryCommand
        {
            get
            {
                return downInCategoryCommand ??
                (downInCategoryCommand = new RelayCommand(obj =>
                {
                    BookViewModel? bookViewModel = obj as BookViewModel;

                    if (bookViewModel != null)
                    {
                        Model.ChangeCategoryListId(bookViewModel.Model, bookViewModel.Model.CategoryListId + 1);
                    }
                }));
            }
        }

        public RelayCommand RemoveFromCategoryCommand
        {
            get
            {
                return removeFromCategoryCommand ??
                (removeFromCategoryCommand = new RelayCommand(obj =>
                {
                    BookViewModel? bookViewModel = obj as BookViewModel;

                    if (bookViewModel != null)
                    {
                        Model.RemoveBookInOrder(bookViewModel.Model);
                    }
                }));
            }
        }

        public RelayCommand OpenUpdateMenuCommand
        {
            get
            {
                return openUpdateMenuCommand ??
                (openUpdateMenuCommand = new RelayCommand(obj =>
                {
                    _updateMenuService.OpenUpdateMenu(this);
                }));
            }
        }

        public RelayCommand RemoveMarkCommand
        {
            get
            {
                return removeMarkCommand ??
                (removeMarkCommand = new RelayCommand(obj =>
                {
                    Model.Mark.RawMark = null;
                }));
            }
        }

        private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
