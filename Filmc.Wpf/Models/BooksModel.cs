using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filmc.Entities.Entities;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.Services;

namespace Filmc.Wpf.Models
{
    public class BooksModel
    {
        private readonly ProfilesService _profilesModel;


        public BooksModel(ProfilesService profilesModel)
        {
            _profilesModel = profilesModel;
            _profilesModel.SelectedProfileChanged += OnSelectedProfileChanged;
        }

        public RepositoriesFacade TablesContext => _profilesModel.SelectedProfile.TablesContext;

        public event Action? TablesContextChanged;

        private void OnSelectedProfileChanged(Profile profile)
        {
            TablesContextChanged?.Invoke();
        }

        public void AddCategory()
        {
            TablesContext.BookCategories.Add();
            TablesContext.SaveChanges();
        }

        public void RemoveCategory(BookCategory category)
        {
            TablesContext.BookCategories.Remove(category);
            TablesContext.SaveChanges();
        }

        public void AddBook()
        {
            Book book = new Book();
            book.GenreId = TablesContext.BookGenres.First().Id;
            TablesContext.Books.Add(book);
            TablesContext.SaveChanges();
        }

        public void AddBook(BookCategory category)
        {
            Book book = new Book();
            book.GenreId = TablesContext.BookGenres.First().Id;

            if (category.HideName != String.Empty)
            {
                book.Name = category.HideName;
            }
            else
            {
                book.Name = category.Name;
            }

            TablesContext.Books.Add(book);

            TablesContext.BookCategories
                .First(x => x.Id == category.Id)
                .AddBookInOrder(book);

            TablesContext.SaveChanges();
        }

        public void DeleteBook(Book book)
        {
            if (book.CategoryId != 0)
                book.CategoryId = 0;

            if (book.GenreId != 0)
                book.GenreId = 0;

            if (book.Priority != null)
                TablesContext.BooksInPriorities.Remove(book.Priority);

            var sources = book.Sources.ToList();
            foreach (var source in sources)
            {
                book.Sources.Remove(source);
                TablesContext.BookSources.Remove(source);
            }

            TablesContext.Books.Remove(book);
            TablesContext.SaveChanges();
        }

        public void AddBookToCategory(BookCategory category, Book book)
        {
            category.AddBookInOrder(book);
            TablesContext.SaveChanges();
        }

        public void RemoveBookFromCategory(BookCategory category, Book book)
        {
            category.RemoveBookInOrder(book);
            TablesContext.SaveChanges();
        }

        public void AddBookToPriority(Book book)
        {
            if (TablesContext.FilmInPriorities.All(x => x.Id != book.Id))
            {
                BooksInPriority priority = new BooksInPriority
                {
                    Id = book.Id,
                    CreationTime = DateTime.Now
                };

                TablesContext.BooksInPriorities.Add(priority);
            }

            TablesContext.SaveChanges();
        }

        public void RemoveBookFromPriority(Book book)
        {
            if (book.Priority != null)
                TablesContext.BooksInPriorities.Remove(book.Priority);

            TablesContext.SaveChanges();
        }

        public void SaveTables()
        {
            _profilesModel.SelectedProfile.SaveTables();
        }
    }
}
