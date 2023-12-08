using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Filmc.Xtl.Tests
{
    public class FilmsTests
    {
        [Fact]
        public void Add_FilmsTable()
        {
            TablesContext context = new TablesContext();

            Film film = new Film { Name = "Test" };
            context.Films.Add(film);

            Assert.Equal(film, context.Films.First());
        }

        [Fact]
        public void Add_FilmsCategoryTable()
        {
            TablesContext context = new TablesContext();

            FilmCategory category = new FilmCategory { Name = "Test" };
            context.FilmCategories.Add(category);

            Assert.Equal(category, context.FilmCategories.First());
        }

        [Fact]
        public void Add_BooksTable()
        {
            TablesContext context = new TablesContext();

            Book book = new Book { Name = "Test" };
            context.Books.Add(book);

            Assert.Equal(book, context.Books.First());
        }

        [Fact]
        public void Add_BooksCategoryTable()
        {
            TablesContext context = new TablesContext();

            BookCategory category = new BookCategory { Name = "Test" };
            context.BookCategories.Add(category);

            Assert.Equal(category, context.BookCategories.First());
        }
    }
}
