using Filmc.Xtl.EntityProperties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Entities
{
    public class Film : Record
    {
        private int _id;
        private string _name;
        private int _genreId;
        private int _realiseYear;
        private bool _isWatched;
        private DateTime _endWatchDate;
        private string _comment;
        private int _countOfViews;
        private int _categoryId;
        private int _categoryListId;

        private DateTime _startWatchDate;
        private int _watchedSeries;
        private int _totalSeries;

        private Mark _mark;
        private ObservableCollection<Source> _sources;

        private FilmCategory? _category;
        private FilmGenre _genre = null!;
        private FilmInPriority? _priority;
        private readonly RecordsCollection<FilmHasTag> _hasTags;

        public Film()
        {
            _id = 0;
            _name = String.Empty;
            _comment = String.Empty;

            _mark = new Mark();
            _sources = new ObservableCollection<Source>();

            _hasTags = new RecordsCollection<FilmHasTag>();
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
        public int RealiseYear 
        { 
            get => _realiseYear; 
            set { _realiseYear = value; OnPropertyChanged(); }
        }
        public bool IsWatched 
        { 
            get => _isWatched; 
            set { _isWatched = value; OnPropertyChanged(); }
        }
        public DateTime EndWatchDate 
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
        public int CategoryId 
        { 
            get => _categoryId; 
            set { _categoryId = value; OnPropertyChanged(); }
        }
        public int CategoryListId 
        { 
            get => _categoryListId; 
            internal set { _categoryListId = value; OnPropertyChanged(); }
        }
        public DateTime StartWatchDate 
        { 
            get => _startWatchDate; 
            set { _startWatchDate = value; OnPropertyChanged(); }
        }
        public int WatchedSeries 
        { 
            get => _watchedSeries; 
            set { _watchedSeries = value; OnPropertyChanged(); }
        }
        public int TotalSeries 
        { 
            get => _totalSeries; 
            set { _totalSeries = value; OnPropertyChanged(); }
        }

        internal int RawMark
        {
            get => Mark.RawMark;
            set => Mark.RawMark = value;
        }

        public Mark Mark
        {
            get => _mark;
            private set => _mark = value;
        }
        public ObservableCollection<Source> Sources
        {
            get => _sources;
            private set => _sources = value;
        }

        public FilmCategory? Category
        { 
            get => _category;
            private set { _category = value; OnPropertyChanged(); }
        }
        public FilmGenre Genre
        {
            get => _genre; 
            private set { _genre = value; OnPropertyChanged(); }
        }
        public FilmInPriority? Priority
        {
            get => _priority;
            private set { _priority = value; OnPropertyChanged(); }
        }
        public RecordsCollection<FilmHasTag> HasTags => _hasTags;

        public override object Clone()
        {
            Film film = new Film();

            film._id = _id;
            film._name = _name;
            film._genreId = _genreId;
            film._realiseYear = _realiseYear;
            film._isWatched = _isWatched;
            film._endWatchDate = _endWatchDate;
            film._comment = _comment;
            film._countOfViews = _countOfViews;
            film._categoryId = _categoryId;
            film._categoryListId = _categoryListId;

            film._startWatchDate = _startWatchDate;
            film._watchedSeries = _watchedSeries;
            film._totalSeries = _totalSeries;

            film._mark.RawMark = _mark.RawMark;
            film._sources = new ObservableCollection<Source>(_sources.Select(x => (Source)x.Clone()));

            return film;
        }
    }
}
