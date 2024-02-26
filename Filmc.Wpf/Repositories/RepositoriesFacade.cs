using Filmc.Entities.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Repositories
{
    public class RepositoriesFacade : IRepositoriesSaved
    {
        private readonly FilmsContext _filmsContext;

        public BookCategoryRepository BookCategories { get; }
        public BookGenreRepository BookGenres { get; }
        public BookInPriorityRepository BooksInPriorities { get; }
        public BookRepository Books { get; }
        public BookSourceRepository BookSources { get; }
        public BookTagRepository BookTags { get; }

        public FilmCategoryRepository FilmCategories { get; }
        public FilmGenreRepository FilmGenres { get; }
        public FilmInPriorityRepository FilmInPriorities { get; }
        public FilmRepository Films { get; }
        public FilmSourceRepository FilmSources { get; }
        public FilmTagRepository FilmTags { get; }

        public IEnumerable<IBaseRepository> Repositories { get; }

        public RepositoriesFacade(string connection)
        {
            var opt = SqliteDbContextOptionsBuilderExtensions.UseSqlite(new DbContextOptionsBuilder(), connection).Options;
            FilmsContext filmsContext = new FilmsContext(opt);

            _filmsContext = filmsContext;

            BookCategories = new BookCategoryRepository(filmsContext.BookCategories);
            BookGenres = new BookGenreRepository(filmsContext.BookGenres);
            BooksInPriorities = new BookInPriorityRepository(filmsContext.BooksInPriorities);
            Books = new BookRepository(filmsContext.Books);
            BookSources = new BookSourceRepository(filmsContext.BookSources);
            BookTags = new BookTagRepository(filmsContext.BookTags);

            FilmCategories = new FilmCategoryRepository(filmsContext.FilmCategories);
            FilmGenres = new FilmGenreRepository(filmsContext.FilmGenres);
            FilmInPriorities = new FilmInPriorityRepository(filmsContext.FilmsInPriorities);
            Films = new FilmRepository(filmsContext.Films);
            FilmSources = new FilmSourceRepository(filmsContext.FilmSources);
            FilmTags = new FilmTagRepository(filmsContext.FilmTags);

            Repositories = new IBaseRepository[] 
            { 
                BookCategories, 
                BookGenres, 
                BooksInPriorities, 
                BookSources, 
                BookTags, 
                FilmCategories, 
                FilmGenres, 
                FilmInPriorities, 
                Films, 
                FilmSources, 
                FilmTags 
            };
        }

        public RepositoriesFacade(FilmsContext filmsContext)
        {
            _filmsContext = filmsContext;

            BookCategories = new BookCategoryRepository(filmsContext.BookCategories);
            BookGenres = new BookGenreRepository(filmsContext.BookGenres);
            BooksInPriorities = new BookInPriorityRepository(filmsContext.BooksInPriorities);
            Books = new BookRepository(filmsContext.Books);
            BookSources = new BookSourceRepository(filmsContext.BookSources);
            BookTags = new BookTagRepository(filmsContext.BookTags);

            FilmCategories = new FilmCategoryRepository(filmsContext.FilmCategories);
            FilmGenres = new FilmGenreRepository(filmsContext.FilmGenres);
            FilmInPriorities = new FilmInPriorityRepository(filmsContext.FilmsInPriorities);
            Films = new FilmRepository(filmsContext.Films);
            FilmSources = new FilmSourceRepository(filmsContext.FilmSources);
            FilmTags = new FilmTagRepository(filmsContext.FilmTags);

            Repositories = new IBaseRepository[]
            {
                BookCategories,
                BookGenres,
                BooksInPriorities,
                BookSources,
                BookTags,
                FilmCategories,
                FilmGenres,
                FilmInPriorities,
                Films,
                FilmSources,
                FilmTags
            };
        }

        public void SaveChanges()
        {
            _filmsContext.SaveChanges();
        }

        public void DeleteDbFile()
        {
            _filmsContext.Database.EnsureDeleted();
        }
    }
}
