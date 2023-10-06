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
    public class Book : Record
    {
        private int _id;
        private string _name;
        private string _author;
        private int _genreId;
        private int _publicationYear;
        private bool _isReaded;
        private DateTime _fullReadDate;
        private int _countOfReadings;
        private string _bookmark;
        private int _categoryId;
        private int _categoryListId;

        private Mark _mark;
        private ObservableCollection<Source> _sources;

        private BookCategory? _category;
        private BookGenre _genre = null!;
        private readonly RecordsCollection<BookHasTag> _hasTags;

        public Book()
        {
            _name = String.Empty;
            _author = String.Empty;
            _bookmark = String.Empty;

            _mark = new Mark();
            _sources = new ObservableCollection<Source>();

            _hasTags = new RecordsCollection<BookHasTag>();
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
        public string Author 
        { 
            get => _author; 
            set { _author = value; OnPropertyChanged(); }
        }
        public int GenreId 
        { 
            get => _genreId; 
            set { _genreId = value; OnPropertyChanged(); }
        }
        public int PublicationYear 
        { 
            get => _publicationYear;
            set { _publicationYear = value; OnPropertyChanged(); }
        }
        public bool IsReaded 
        { 
            get => _isReaded; 
            set { _isReaded = value; OnPropertyChanged(); }
        }
        public DateTime FullReadDate 
        { 
            get => _fullReadDate; 
            set { _fullReadDate = value; OnPropertyChanged(); }
        }
        public int CountOfReadings 
        { 
            get => _countOfReadings; 
            set { _countOfReadings = value; OnPropertyChanged(); }
        }
        public string Bookmark 
        { 
            get => _bookmark; 
            set { _bookmark = value; OnPropertyChanged(); }
        }
        public int CategoryId 
        { 
            get => _categoryId; 
            set { _categoryId = value; OnPropertyChanged(); }
        }
        public int CategoryListId 
        { 
            get => _categoryListId; 
            set { _categoryListId = value; OnPropertyChanged(); }
        }

        internal int RawMark
        {
            get => Mark.RawMark;
            set => Mark.RawMark = value;
        }

        public Mark Mark 
        { 
            get => _mark;
            private set { _mark = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Source> Sources 
        { 
            get => _sources; 
            private set { _sources = value; OnPropertyChanged(); }
        }

        public BookCategory? Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }
        public BookGenre Genre
        {
            get => _genre;
            set { _genre = value; OnPropertyChanged(); }
        }
        public RecordsCollection<BookHasTag> HasTags => _hasTags;

        public override object Clone()
        {
            Book book = new Book();

            book._id = _id;
            book._name = _name;
            book._author = _author;
            book._genreId = _genreId;
            book._publicationYear = _publicationYear;
            book._isReaded = _isReaded;
            book._fullReadDate = _fullReadDate;
            book._countOfReadings = _countOfReadings;
            book._bookmark = _bookmark;
            book._categoryId = _categoryId;
            book._categoryListId = _categoryListId;

            book._mark.RawMark = _mark.RawMark;
            book._sources = new ObservableCollection<Source>(_sources.Select(x => (Source)x.Clone()));

            return book;
        }
    }
}
