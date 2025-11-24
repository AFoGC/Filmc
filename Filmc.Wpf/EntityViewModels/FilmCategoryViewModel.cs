using Filmc.Entities.Entities;
using Filmc.Entities.Repositories;
using Filmc.Wpf.Commands;
using Filmc.Wpf.Services;
using Filmc.Wpf.ViewCollections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.EntityViewModels
{
    public class FilmCategoryViewModel : BaseEntityViewModel
    {
        public FilmCategory Model { get; }

        private readonly UpdateMenuService _updateMenuService;
        private readonly IRepositoriesSaved _repositories;

        private bool _isCollectionVisible;
        private bool _isSelected;

        public FilmCategoryViewModel(FilmCategory model, ObservableCollection<FilmViewModel> filmsViewModel, 
                                     UpdateMenuService updateMenuService, IRepositoriesSaved repositories)
        {
            Model = model;
            Model.PropertyChanged += OnModelPropertyChanged;
            Model.Mark.PropertyChanged += OnModelPropertyChanged;

            _isCollectionVisible = true;
            FilmsVC = new FilmsInCategoryViewCollection(model, filmsViewModel);

            _updateMenuService = updateMenuService;
            _repositories = repositories;

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

        public FilmsInCategoryViewCollection FilmsVC { get; }

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
            FilmViewModel? filmViewModel = obj as FilmViewModel;

            if (filmViewModel != null)
            {
                UpInCategory(filmViewModel);
            }
        }

        public void UpInCategory(FilmViewModel filmViewModel)
        {
            Model.ChangeCategoryListId(filmViewModel.Model, filmViewModel.Model.CategoryListId - 1);
            _repositories.SaveChanges();
        }

        public void DownInCategory(object? obj)
        {
            FilmViewModel? filmViewModel = obj as FilmViewModel;

            if (filmViewModel != null)
            {
                DownInCategory(filmViewModel);
            }
        }

        public void DownInCategory(FilmViewModel filmViewModel)
        {
            Model.ChangeCategoryListId(filmViewModel.Model, filmViewModel.Model.CategoryListId + 1);
            _repositories.SaveChanges();
        }

        public void RemoveFromCategory(object? obj)
        {
            FilmViewModel? filmViewModel = obj as FilmViewModel;

            if (filmViewModel != null)
            {
                RemoveFromCategory(filmViewModel);
            }
        }

        public void RemoveFromCategory(FilmViewModel filmViewModel)
        {
            Model.RemoveFilmInOrder(filmViewModel.Model);
            _repositories.SaveChanges();
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
