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
using Xtl;

namespace Filmc.Wpf.EntityViewModels
{
    public class FilmViewModel : BaseEntityViewModel, IHasSourcesViewModel
    {
        public Film Model { get; }

        private readonly UpdateMenuService _updateMenuService;

        private bool _isSelected;

        private RelayCommand? copyUrlCommand;
        private RelayCommand? openUpdateMenuCommand;
        private RelayCommand? removeMarkCommand;

        public FilmViewModel(Film model, UpdateMenuService updateMenuService)
        {
            Model = model;
            Model.PropertyChanged += OnModelPropertyChanged;
            Model.Mark.PropertyChanged += OnModelPropertyChanged;
            Model.Sources.CollectionChanged += OnSourcesCollectionChanged;
            AddCategoryPropertyChanged();

            _updateMenuService = updateMenuService;

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
        public RecordsCollection<FilmHasTag> HasTags
        {
            get => Model.HasTags;
        }
        
        public string ShortName
        {
            get
            {
                if (Model.Category != null)
                {
                    FilmCategory category = Model.Category;

                    if (category.HideName != String.Empty)
                        return Model.Name.Replace(category.HideName, String.Empty);

                    if (category.Name != String.Empty)
                        return Model.Name.Replace(category.Name, String.Empty);
                }

                return Model.Name;
            }
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

        private void RemoveCategoryPropertyChanged()
        {
            if (Model.Category != null)
                Model.Category.PropertyChanged -= OnCategoryPropertyChanged;
        }

        private void AddCategoryPropertyChanged()
        {
            if (Model.Category != null)
                Model.Category.PropertyChanged += OnCategoryPropertyChanged;
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

            if (e.PropertyName == nameof(Model.CategoryId))
            {
                RemoveCategoryPropertyChanged();
            }

            if (e.PropertyName == nameof(Model.Category))
            {
                AddCategoryPropertyChanged();
                OnPropertyChanged(nameof(ShortName));
            }

            if (e.PropertyName == nameof(Model.Name))
            {
                OnPropertyChanged(nameof(ShortName));
            }
        }

        private void OnCategoryPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(ShortName));
        }
    }
}
