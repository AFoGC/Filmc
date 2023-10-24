using Filmc.Xtl.EntityProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Entities
{
    public class BookCategory : Record
    {
        private int _id;
        private string _name;
        private string _hideName;
        private int _priority;

        private Mark _mark;

        private readonly RecordsCollection<Book> _books;

        public BookCategory()
        {
            _id = 0;
            _name = String.Empty;
            _hideName = String.Empty;
            _priority = 0;

            _mark = new Mark();

            _books = new RecordsCollection<Book>();
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
        public int Priority
        {
            get => _priority;
            set { _priority = value; OnPropertyChanged(); }
        }

        internal int? RawMark
        {
            get => Mark.RawMark;
            set => Mark.RawMark = value;
        }
        public Mark Mark
        {
            get => _mark;
            private set => _mark = value;
        }

        public RecordsCollection<Book> Books => _books;

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

        public bool ChangeCategoryListId(Book book, int newListId)
        {
            if (Books.Contains(book))
            {
                var plusCollection = Books
                    .Where(x => x.CategoryListId >= newListId && x.CategoryListId < book.CategoryListId)
                    .OrderBy(x => x.CategoryListId);

                var minusCollection = Books
                    .Where(x => x.CategoryListId <= newListId && x.CategoryListId > book.CategoryListId)
                    .OrderBy(x => x.CategoryListId);

                foreach (Book item in plusCollection)
                    item.CategoryListId++;

                foreach (Book item in minusCollection)
                    item.CategoryListId--;

                if (newListId < 0)
                    newListId = 0;

                if (newListId >= Books.Count)
                    newListId = Books.Count - 1;

                book.CategoryListId = newListId;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override object Clone()
        {
            BookCategory category = new BookCategory();

            category._id = _id;
            category._name = _name;
            category._hideName = _hideName;
            category._priority = _priority;

            category._mark.RawMark = _mark.RawMark;

            return category;
        }
    }
}
