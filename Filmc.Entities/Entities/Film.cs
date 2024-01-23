using System;
using System.Collections.Generic;
using Filmc.Entities.PropertyTypes;

namespace Filmc.Entities.Entities
{
    public partial class Film : BaseEntity
    {
        private int _id;
        private string _name = null!;
        private int _genreId;
        private int? _realiseYear;
        private bool _isWatched;
        private DateTime? _endWatchDate;
        private string _comment = null!;
        private int _countOfViews;
        private int? _categoryId;
        private int? _categoryListId;
        private bool _isOnTheBlacklist;
        private bool _isOnSecretMode;
        private DateTime? _startWatchDate;
        private int? _watchedSeries;
        private int? _totalSeries;

        public Film()
        {
            Name = String.Empty;
            Comment = String.Empty;
            Mark = new Mark();

            FilmSources = new HashSet<FilmSource>();
            Tags = new HashSet<FilmTag>();
        }

        public int Id 
        { 
            get => _id; 
            set { _id = value; OnPropertyChanged(); }
        }
        public string Name 
        { 
            get => _name; 
            set { _name = value; OnPropertyChanged(); }
        }
        public int GenreId 
        { 
            get => _genreId; 
            set { _genreId = value; OnPropertyChanged(); }
        }
        public int? RealiseYear 
        { 
            get => _realiseYear; 
            set { _realiseYear = value; OnPropertyChanged(); }
        }
        public bool IsWatched 
        { 
            get => _isWatched; 
            set { _isWatched = value; OnPropertyChanged(); }
        }
        internal int? RawMark
        {
            get => Mark.RawMark;
            set => Mark.RawMark = value;
        }
        public DateTime? EndWatchDate 
        { 
            get => _endWatchDate; 
            set { _endWatchDate = value; OnPropertyChanged(); }
        }
        public string Comment 
        { 
            get => _comment; 
            set { _comment = value; OnPropertyChanged(); }
        }
        public int CountOfViews 
        { 
            get => _countOfViews; 
            set { _countOfViews = value; OnPropertyChanged(); }
        }
        public int? CategoryId 
        { 
            get => _categoryId; 
            set { _categoryId = value; OnPropertyChanged(); }
        }
        public int? CategoryListId 
        { 
            get => _categoryListId; 
            set { _categoryListId = value; OnPropertyChanged(); }
        }
        public bool IsOnTheBlacklist 
        { 
            get => _isOnTheBlacklist; 
            set { _isOnTheBlacklist = value; OnPropertyChanged(); }
        }
        public bool IsOnSecretMode 
        { 
            get => _isOnSecretMode; 
            set { _isOnSecretMode = value; OnPropertyChanged(); }
        }
        public DateTime? StartWatchDate 
        { 
            get => _startWatchDate; 
            set { _startWatchDate = value; OnPropertyChanged(); }
        }
        public int? WatchedSeries 
        { 
            get => _watchedSeries; 
            set { _watchedSeries = value; OnPropertyChanged(); }
        }
        public int? TotalSeries 
        { 
            get => _totalSeries; 
            set { _totalSeries = value; OnPropertyChanged(); }
        }

        public virtual Mark Mark { get; }

        public virtual FilmCategory? Category { get; set; }
        public virtual FilmGenre Genre { get; set; } = null!;
        public virtual FilmsInPriority? FilmsInPriority { get; set; }
        public virtual ICollection<FilmSource> FilmSources { get; set; }

        public virtual ICollection<FilmTag> Tags { get; set; }
    }
}
