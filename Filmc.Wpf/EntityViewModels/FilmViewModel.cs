using Filmc.Wpf.Commands;
using Filmc.Wpf.Helper;
using Filmc.Wpf.Services;
using Filmc.Wpf.ViewModels;
using Filmc.Xtl.Entities;
using Filmc.Xtl.EntityProperties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Filmc.Wpf.EntityViewModels
{
    public class FilmViewModel : BaseEntityViewModel
    {
        public Film Model { get; }

        private readonly UpdateMenuService _updateMenuService;

        private bool _isCommentVisible;
        private bool _isSelected;

        private RelayCommand? copyUrlCommand;
        private RelayCommand? openCommentCommand;
        private RelayCommand? openUpdateMenuCommand;

        public FilmViewModel(Film model, UpdateMenuService updateMenuService)
        {
            Model = model;
            Model.PropertyChanged += OnModelPropertyChanged;
            Model.Mark.PropertyChanged += OnModelPropertyChanged;
            Model.Sources.CollectionChanged += OnSourcesCollectionChanged;

            _updateMenuService = updateMenuService;

            _isCommentVisible = false;
            _isSelected = false;
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
        public FilmGenre Genre
        {
            get => Model.Genre;
            set => Model.GenreId = value.Id;
        }
        public int RealiseYear
        {
            get => Model.RealiseYear;
            set => Model.RealiseYear = value;
        }
        public bool IsWatched
        {
            get => Model.IsWatched;
            set => Model.IsWatched = value;
        }
        public DateTime? EndWatchDate
        {
            get => Model.EndWatchDate;
            set => Model.EndWatchDate = value;
        }
        public string Comment
        {
            get => Model.Comment;
            set => Model.Comment = value;
        }
        public int CountOfViews
        {
            get => Model.CountOfViews;
            set => Model.CountOfViews = value;
        }
        public int CategoryListId
        {
            get => Model.CategoryListId;
        }
        public DateTime? StartWatchDate
        {
            get => Model.StartWatchDate;
            set => Model.StartWatchDate = value;
        }
        public int WatchedSeries
        {
            get => Model.WatchedSeries;
            set => Model.WatchedSeries = value;
        }
        public int TotalSeries
        {
            get => Model.TotalSeries;
            set => Model.TotalSeries = value;
        }
        public int CategoryId
        {
            get => Model.CategoryId;
        }
        public ObservableCollection<Source> Sources
        {
            get => Model.Sources;
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

        public bool HasPriority
        {
            get => Model.Priority != null;
        }
        public DateTime? AddToPriorityTime
        {
            get => Model.Priority?.CreationTime;
        }

        public bool IsCommentVisible
        {
            get => _isCommentVisible;
            set { _isCommentVisible = value; OnPropertyChanged(); }
        }
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }
        public string SourcesText
        {
            get
            {
                if (Model.Sources.Count != 0)
                {
                    if (Model.Sources[0].Name == String.Empty)
                    {
                        return "Copy url";
                    }
                    else
                    {
                        return Model.Sources[0].Name;
                    }
                }
                else
                {
                    return "No url";
                }
            }
        }

        public RelayCommand CopyUrlCommand
        {
            get
            {
                return copyUrlCommand ??
                (copyUrlCommand = new RelayCommand(obj =>
                {
                    ClipboardHelper.CopySourceUrlToClipboard(Model.Sources);
                }));
            }
        }

        public RelayCommand OpenCommentCommand
        {
            get
            {
                return openCommentCommand ??
                (openCommentCommand = new RelayCommand(obj =>
                {
                    IsCommentVisible = !IsCommentVisible;
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

        private void OnSourcesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SourcesText));
        }

        private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);

            if (e.PropertyName == nameof(Model.Priority))
            {
                OnPropertyChanged(nameof(HasPriority));
                OnPropertyChanged(nameof(AddToPriorityTime));
            }
        }
    }
}
