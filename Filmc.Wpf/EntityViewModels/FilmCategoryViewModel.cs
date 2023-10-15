﻿using Filmc.Wpf.Commands;
using Filmc.Wpf.ViewCollections;
using Filmc.Xtl.Entities;
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

        private bool _isCollectionVisible;
        private bool _isSelected;

        private RelayCommand? createFilmCommand;
        private RelayCommand? collapseCommand;
        private RelayCommand? openedContextMenuCommand;
        private RelayCommand? closedContextMenuCommand;

        public FilmCategoryViewModel(FilmCategory model, ObservableCollection<FilmViewModel> filmsViewModel)
        {
            Model = model;
            Model.PropertyChanged += OnModelPropertyChanged;
            Model.Mark.PropertyChanged += OnModelPropertyChanged;

            _isCollectionVisible = true;
            FilmsVC = new FilmsInCategoryViewCollection(model, filmsViewModel);
        }

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

        public int FormatedMark
        {
            get => Model.Mark.FormatedMark;
            set => Model.Mark.FormatedMark = value;
        }
        public int MarkSystem
        {
            get => Model.Mark.MarkSystem;
            set => Model.Mark.MarkSystem = value;
        }
        public int RawMark
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

        public override bool Search(string search)
        {
            throw new NotImplementedException();
        }

        private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
