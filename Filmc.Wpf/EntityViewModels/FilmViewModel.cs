using Filmc.Entities.Entities;
using Filmc.Wpf.Commands;
using Filmc.Wpf.Helper;
using Filmc.Wpf.Services;
using Filmc.Wpf.ViewModels;
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
    public class FilmViewModel : BaseEntityViewModel, IHasSourcesViewModel
    {
        public Film Model { get; }

        private readonly UpdateMenuService _updateMenuService;

        private bool _isSelected;

        public FilmViewModel(Film model, UpdateMenuService updateMenuService)
        {
            Model = model;
            Model.PropertyChanged += OnModelPropertyChanged;
            Model.Mark.PropertyChanged += OnModelPropertyChanged;
            Model.Sources.CollectionChanged += OnSourcesCollectionChanged;
            AddCategoryPropertyChanged();

            _updateMenuService = updateMenuService;

            _isSelected = false;

            CopyUrlCommand = new RelayCommand(CopyUrl);
            OpenUpdateMenuCommand = new RelayCommand(OpenUpdateMenu);
            RemoveMarkCommand = new RelayCommand(RemoveMark);
            FilmTags = new FilmTagsService(model.Tags);
        }

        public RelayCommand CopyUrlCommand { get; }
        public RelayCommand OpenUpdateMenuCommand { get; }
        public RelayCommand RemoveMarkCommand { get; }

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
            set => Model.Genre = value;
        }
        public int? RealiseYear
        {
            get => Model.RealiseYear;
            set => Model.RealiseYear = value;
        }
        public FilmWatchProgress WatchProgress
        {
            get => Model.WatchProgress;
            set => Model.WatchProgress = value;
        }
        public DateTime? EndWatchDate
        {
            get => Model.EndWatchDate;
            set
            {
                if (value != null)
                {
                    DateTime now = DateTime.Now;
                    DateTime val = (DateTime)value;
                    Model.EndWatchDate = new DateTime(val.Year, val.Month, val.Day, now.Hour, now.Minute, now.Second);
                }
                else
                {
                    Model.EndWatchDate = null;
                }
            }
        }
        public string Comment
        {
            get => Model.Comment;
            set => Model.Comment = value;
        }
        public int? CountOfViews
        {
            get => Model.CountOfViews;
            set => Model.CountOfViews = value;
        }
        public int? CategoryListId
        {
            get => Model.CategoryListId;
        }
        public DateTime? StartWatchDate
        {
            get => Model.StartWatchDate;
            set
            {
                if (value != null)
                {
                    DateTime now = DateTime.Now;
                    DateTime val = (DateTime)value;
                    Model.StartWatchDate = new DateTime(val.Year, val.Month, val.Day, now.Hour, now.Minute, now.Second);
                }
                else
                {
                    Model.StartWatchDate = null;
                }
            }
        }
        public int? WatchedSeries
        {
            get => Model.WatchedSeries;
            set => Model.WatchedSeries = value;
        }
        public int? TotalSeries
        {
            get => Model.TotalSeries;
            set => Model.TotalSeries = value;
        }
        public int? CategoryId
        {
            get => Model.CategoryId;
        }
        public ObservableCollection<FilmSource> Sources
        {
            get => Model.Sources;
        }
        public ObservableCollection<FilmTag> HasTags
        {
            get => Model.Tags;
        }

        public FilmTagsService FilmTags { get; }
        
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
                    var source = Model.Sources
                        .OrderBy(x => x.IndexInList)
                        .First();

                    if (source.Name == String.Empty)
                    {
                        return "Copy url";
                    }
                    else
                    {
                        return source.Name;
                    }
                }
                else
                {
                    return "No url";
                }
            }
        }

        public void CopyUrl(object? obj)
        {
            ClipboardHelper.CopySourceUrlToClipboard(Model.Sources);
        }
        
        public void OpenUpdateMenu(object? obj)
        {
            _updateMenuService.OpenUpdateMenu(this);
        }

        public void RemoveMark(object? obj)
        {
            Model.Mark.RawMark = null;
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

        public void AddSource(Profile profile)
        {
            FilmSource source = new FilmSource();

            source.IndexInList = Model.Sources.Count;
            Model.Sources.Add(source);

            profile.TablesContext.SaveChanges();
        }

        public void RemoveSource(ISource source, Profile profile)
        {
            FilmSource? filmSource = source as FilmSource;

            if (filmSource != null)
            {
                Model.Sources.Remove(filmSource);

                var items = Model.Sources
                    .OrderBy(x => x.IndexInList);

                int i = 0;
                foreach (var item in items)
                {
                    item.IndexInList = i;
                    i++;
                }

                profile.TablesContext.FilmSources.Remove(filmSource);
                profile.TablesContext.SaveChanges();
            }
        }

        public void SetFirstSource(ISource source, Profile profile)
        {
            FilmSource? filmSource = source as FilmSource;

            if (filmSource != null)
            {
                filmSource.IndexInList = 0;
                int i = 1;

                var items = Model.Sources
                    .OrderBy(x => x.IndexInList)
                    .Where(x => x.Id != filmSource.Id);

                foreach (var item in items)
                {
                    item.IndexInList = i;
                    i++;
                }

                profile.TablesContext.SaveChanges();
                OnPropertyChanged(nameof(SourcesText));
            }
        }
    }
}
