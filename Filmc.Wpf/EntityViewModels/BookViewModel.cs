using Filmc.Entities.Entities;
using Filmc.Wpf.Commands;
using Filmc.Wpf.Helper;
using Filmc.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.EntityViewModels
{
    public class BookViewModel : BaseEntityViewModel, IHasSourcesViewModel
    {
        public Book Model { get; }

        private readonly UpdateMenuService _updateMenuService;

        private bool _isCommentVisible;
        private bool _isSelected;

        private RelayCommand? copyUrlCommand;
        private RelayCommand? openCommentCommand;
        private RelayCommand? openUpdateMenuCommand;
        private RelayCommand? removeMarkCommand;

        public BookViewModel(Book model, UpdateMenuService updateMenuService)
        {
            Model = model;
            Model.PropertyChanged += OnModelPropertyChanged;
            Model.Mark.PropertyChanged += OnModelPropertyChanged;
            Model.Sources.CollectionChanged += OnSourcesCollectionChanged;
            AddCategoryPropertyChanged();

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
        public string Author
        {
            get => Model.Author;
            set => Model.Author = value;
        }
        public BookGenre Genre
        {
            get => Model.Genre;
            set => Model.GenreId = value.Id;
        }
        public int? PublicationYear
        {
            get => Model.PublicationYear;
            set => Model.PublicationYear = value;
        }
        public bool IsReaded
        {
            get => Model.IsReaded;
            set => Model.IsReaded = value;
        }
        public DateTime? FullReadDate
        {
            get => Model.EndReadDate;
            set
            {
                if (value != null)
                {
                    DateTime now = DateTime.Now;
                    DateTime val = (DateTime)value;
                    Model.EndReadDate = new DateTime(val.Year, val.Month, val.Day, now.Hour, now.Minute, now.Second);
                }
                else
                {
                    Model.EndReadDate = null;
                }
            }
        }
        public int? CountOfReadings
        {
            get => Model.CountOfReadings;
            set => Model.CountOfReadings = value;
        }
        public string Bookmark
        {
            get => Model.Bookmark;
            set => Model.Bookmark = value;
        }
        public int? CategoryId
        {
            get => Model.CategoryId;
        }
        public int? CategoryListId
        {
            get => Model.CategoryListId;
        }
        public ObservableCollection<BookSource> Sources
        {
            get => Model.Sources;
        }
        public ObservableCollection<BookTag> HasTags
        {
            get => Model.Tags;
        }

        public string ShortName
        {
            get
            {
                if (Model.Category != null)
                {
                    BookCategory category = Model.Category;

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

        public void AddSource()
        {
            BookSource source = new BookSource();
            Model.Sources.Add(source);
        }

        public void RemoveSource(ISource source)
        {
            BookSource? filmSource = source as BookSource;

            if (filmSource != null)
                Model.Sources.Remove(filmSource);
        }

        public void SetFirstSource(ISource source)
        {
            BookSource? filmSource = source as BookSource;

            if (filmSource != null)
            {
                Model.Sources.Remove(filmSource);
                Model.Sources.Insert(0, filmSource);
            }
        }
    }
}
