using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Filmc.Entities.PropertyTypes;

namespace Filmc.Entities.Entities
{
    public partial class BookCategory : BaseEntity
    {
        private int _id;
        private string _name = null!;
        private string _hideName = null!;

        public BookCategory()
        {
            Name = String.Empty;
            HideName = String.Empty;
            Mark = new Mark();

            Books = new NotifyCollection<Book>();
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
        public string HideName 
        { 
            get => _hideName; 
            set { _hideName = value; OnPropertyChanged(); }
        }
        internal int? RawMark
        {
            get => Mark.RawMark;
            set => Mark.RawMark = value;
        }

        public virtual Mark Mark { get; }

        public virtual NotifyCollection<Book> Books { get; }
        public virtual INotifyCollection<Book> CategoryBooks => Books;

        public void AddBookInOrder(Book book)
        {
            if (Books.Contains(book) == false)
            {
                book.CategoryListId = Books.Count;
                Books.Add(book);
            }
        }

        public void RemoveBookInOrder(Book book)
        {
            if (Books.Remove(book))
            {
                var sortedFilms = Books.OrderBy(x => x.CategoryListId);

                int i = 0;
                foreach (Book item in sortedFilms)
                    item.CategoryListId = i++;
            }
        }

        public bool ChangeCategoryListId(Book book, int? newListId)
        {
            if (Books.Contains(book) && newListId != null)
            {
                if (newListId < 0)
                    newListId = 0;

                if (newListId >= Books.Count)
                    newListId = Books.Count - 1;

                var plusCollection = Books
                    .Where(x => x.CategoryListId >= newListId && x.Id != book.Id)
                    .OrderBy(x => x.CategoryListId);

                var minusCollection = Books
                    .Where(x => x.CategoryListId < newListId && x.Id != book.Id)
                    .OrderByDescending(x => x.CategoryListId);

                book.CategoryListId = newListId;

                int i = (int)newListId;
                foreach (Book item in plusCollection)
                {
                    i++;
                    item.CategoryListId = i;
                }

                i = (int)newListId;
                foreach (Book item in minusCollection)
                {
                    i--;
                    item.CategoryListId = i;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
