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

            CopyUrlCommand = new RelayCommand(CopyUrl);
            OpenCommentCommand = new RelayCommand(OpenComment);
            OpenUpdateMenuCommand = new RelayCommand(OpenUpdateMenu);
            RemoveMarkCommand = new RelayCommand(RemoveMark);
        }

        public RelayCommand CopyUrlCommand { get; }
        public RelayCommand OpenCommentCommand { get; }
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
        public string Author
        {
            get => Model.Author;
            set => Model.Author = value;
        }
        public BookGenre Genre
        {
            get => Model.Genre;
            set => Model.Genre = value;
        }
        public int? PublicationYear
        {
            get => Model.PublicationYear;
            set => Model.PublicationYear = value;
        }
        public BookReadProgress ReadProgress
        {
            get => Model.ReadProgress;
            set => Model.ReadProgress = value;
        }
        public DateTime? StartReadDate
        {
            get => Model.StartReadDate;
            set
            {
                if (value != null)
                {
                    DateTime now = DateTime.Now;
                    DateTime val = (DateTime)value;
                    Model.StartReadDate = new DateTime(val.Year, val.Month, val.Day, now.Hour, now.Minute, now.Second);
                }
                else
                {
                    Model.StartReadDate = null;
                }
            }
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

        public void OpenComment(object? obj)
        {
            IsCommentVisible = !IsCommentVisible;
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
            BookSource source = new BookSource();

            source.IndexInList = Model.Sources.Count;
            Model.Sources.Add(source);

            profile.TablesContext.SaveChanges();
        }

        public void RemoveSource(ISource source, Profile profile)
        {
            BookSource? bookSource = source as BookSource;

            if (bookSource != null)
            {
                Model.Sources.Remove(bookSource);

                var items = Model.Sources
                    .OrderBy(x => x.IndexInList);

                int i = 0;
                foreach (var item in items)
                {
                    item.IndexInList = i;
                    i++;
                }

                profile.TablesContext.BookSources.Remove(bookSource);
                profile.TablesContext.SaveChanges();
            }
        }

        public void SetFirstSource(ISource source, Profile profile)
        {
            BookSource? filmSource = source as BookSource;

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
