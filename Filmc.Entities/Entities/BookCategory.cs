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
            if (book.CategoryId == null)
            {
                book.CategoryListId = Books.Count;
                book.CategoryId = this.Id;
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

                int? currentListId = book.CategoryListId;

                while (book.CategoryListId != newListId)
                {
                    if (book.CategoryListId > newListId)
                    {
                        currentListId = book.CategoryListId - 1;
                        Books.First(x => x.CategoryListId == currentListId).CategoryListId++;
                        book.CategoryListId--;
                    }

                    if (book.CategoryListId < newListId)
                    {
                        currentListId = book.CategoryListId + 1;
                        Books.First(x => x.CategoryListId == currentListId).CategoryListId--;
                        book.CategoryListId++;
                    }
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
