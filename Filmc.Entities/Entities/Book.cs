using System;
using System.Collections.Generic;
using Filmc.Entities.PropertyTypes;

namespace Filmc.Entities.Entities
{
    public partial class Book
    {
        private int _id;
        private string _name = null!;
        private string _author = null!;
        private int _genreId;
        private int? _publicationYear;
        private bool _isReaded;
        private DateTime? _startReadDate;
        private DateTime? _endReadDate;
        private string _comment = null!;
        private int? _countOfReadings;
        private string _bookmark = null!;
        private int? _categoryId;
        private int? _categoryListId;
        private bool _isOnTheBlacklist;
        private bool _isOnSecretMode;

        public Book()
        {
            Name = String.Empty;
            Author = String.Empty;
            Comment = String.Empty;
            Bookmark = String.Empty;
            Mark = new Mark();

            BookSources = new HashSet<BookSource>();
            Tags = new HashSet<BookTag>();
        }

        public int Id
        { 
            get => _id; 
            set => _id = value; 
        }
        public string Name 
        { 
            get => _name; 
            set => _name = value; 
        }
        public string Author 
        { 
            get => _author; 
            set => _author = value; 
        }
        public int GenreId 
        { 
            get => _genreId; 
            set => _genreId = value; 
        }
        public int? PublicationYear 
        { 
            get => _publicationYear; 
            set => _publicationYear = value; 
        }
        public bool IsReaded 
        { 
            get => _isReaded;
            set => _isReaded = value; 
        }
        public DateTime? StartReadDate 
        { 
            get => _startReadDate; 
            set => _startReadDate = value; 
        }
        public DateTime? EndReadDate 
        { 
            get => _endReadDate; 
            set => _endReadDate = value; 
        }
        public int? RawMark
        {
            get => Mark.RawMark;
            set => Mark.RawMark = value;
        }
        public string Comment 
        { 
            get => _comment; 
            set => _comment = value; 
        }
        public int? CountOfReadings 
        { 
            get => _countOfReadings; 
            set => _countOfReadings = value; 
        }
        public string Bookmark 
        { 
            get => _bookmark; 
            set => _bookmark = value; 
        }
        public int? CategoryId 
        { 
            get => _categoryId; 
            set => _categoryId = value; 
        }
        public int? CategoryListId 
        { 
            get => _categoryListId; 
            set => _categoryListId = value; 
        }
        public bool IsOnTheBlacklist 
        { 
            get => _isOnTheBlacklist; 
            set => _isOnTheBlacklist = value; 
        }
        public bool IsOnSecretMode 
        { 
            get => _isOnSecretMode; 
            set => _isOnSecretMode = value; 
        }

        public virtual Mark Mark { get; }

        public virtual BookCategory? Category { get; set; }
        public virtual BookGenre Genre { get; set; } = null!;
        public virtual BooksInPriority? BooksInPriority { get; set; }
        public virtual ICollection<BookSource> BookSources { get; set; }

        public virtual ICollection<BookTag> Tags { get; set; }
    }
}
