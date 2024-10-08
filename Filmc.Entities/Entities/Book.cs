﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Filmc.Entities.PropertyTypes;

namespace Filmc.Entities.Entities
{
    public partial class Book : BaseEntity
    {
        private int _id;
        private string _name = null!;
        private string _author = null!;
        private int _genreId;
        private int? _publicationYear;
        private int _readProgressId;
        private DateTime? _startReadDate;
        private DateTime? _endReadDate;
        private string _comment = null!;
        private int? _countOfReadings;
        private string _bookmark = null!;
        private int? _categoryId;
        private int? _categoryListId;
        private bool _isOnTheBlacklist;
        private bool _isOnSecretMode;

        private BookCategory? category;
        private BookGenre genre = null!;
        private BooksInPriority? booksInPriority;
        private BookReadProgress readProgress = null!;

        public Book()
        {
            Name = String.Empty;
            Author = String.Empty;
            Comment = String.Empty;
            Bookmark = String.Empty;
            Mark = new Mark();

            Sources = new ObservableCollection<BookSource>();
            Tags = new ObservableCollection<BookTag>();
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
        public int? PublicationYear
        {
            get => _publicationYear;
            set { _publicationYear = value; OnPropertyChanged(); }
        }
        public int ReadProgressId
        {
            get => _readProgressId;
            set { _readProgressId = value; OnPropertyChanged(); }
        }
        public DateTime? StartReadDate
        {
            get => _startReadDate;
            set { _startReadDate = value; OnPropertyChanged(); }
        }
        public DateTime? EndReadDate
        {
            get => _endReadDate;
            set { _endReadDate = value; OnPropertyChanged(); }
        }
        internal int? RawMark
        {
            get => Mark.RawMark;
            set => Mark.RawMark = value;
        }
        public string Comment
        {
            get => _comment;
            set { _comment = value; OnPropertyChanged(); }
        }
        public int? CountOfReadings
        {
            get => _countOfReadings;
            set { _countOfReadings = value; OnPropertyChanged(); }
        }
        public string Bookmark
        {
            get => _bookmark;
            set { _bookmark = value; OnPropertyChanged(); }
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

        public virtual Mark Mark { get; }

        public virtual BookCategory? Category
        {
            get => category;
            set { category = value; OnPropertyChanged(); }
        }
        public virtual BookGenre Genre
        { 
            get => genre;
            set { genre = value; OnPropertyChanged(); }
        }
        public virtual BooksInPriority? Priority 
        { 
            get => booksInPriority;
            set { booksInPriority = value; OnPropertyChanged(); } 
        }
        public virtual BookReadProgress ReadProgress
        {
            get => readProgress;
            set { readProgress = value; OnPropertyChanged(); }
        }

        public virtual ObservableCollection<BookSource> Sources { get; }
        public virtual ObservableCollection<BookTag> Tags { get; }
    }
}
