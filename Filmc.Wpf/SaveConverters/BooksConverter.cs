using Filmc.Entities.Context;
using Filmc.Xtl.Entities;
using Filmc.Xtl.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.SaveConverters
{
    public static class BooksConverter
    {
        public static void ConvertBookCategories(FilmsContext filmsContext, BookCategoriesTable destination)
        {
            foreach (BookCategory item in destination)
            {
                Entities.Entities.BookCategory entity = new Entities.Entities.BookCategory
                {
                    Id = item.Id,
                    Name = item.Name,
                    HideName = item.HideName
                };

                entity.Mark.RawMark = item.Mark.RawMark;

                filmsContext.BookCategories.Local.Add(entity);
            }
        }

        public static void ConvertBookGenres(FilmsContext filmsContext, BookGenresTable destination)
        {
            foreach (BookGenre item in destination)
            {
                Entities.Entities.BookGenre entity = new Entities.Entities.BookGenre
                {
                    Id = item.Id,
                    Name = item.Name
                };

                filmsContext.BookGenres.Local.Add(entity);
            }
        }

        public static void ConvertBooks(FilmsContext filmsContext, BooksTable destination)
        {
            foreach (Book item in destination)
            {
                Entities.Entities.Book entity = new Entities.Entities.Book
                {
                    Id = item.Id,
                    Name = item.Name,
                    Author = item.Author,
                    GenreId = item.GenreId
                };

                if (item.PublicationYear != 0)
                    entity.PublicationYear = item.PublicationYear;

                entity.IsReaded = item.IsReaded;
                entity.EndReadDate = item.FullReadDate;
                entity.CountOfReadings = item.CountOfReadings;
                entity.Bookmark = item.Bookmark;

                entity.Mark.RawMark = item.Mark.RawMark;

                filmsContext.Books.Local.Add(entity);

                if (item.CategoryId != 0)
                    filmsContext.BookCategories.Local
                        .First(x => x.Id == item.CategoryId)
                        .AddBookInOrder(entity);

                foreach (var source in item.Sources)
                {
                    Entities.Entities.BookSource bookSource = new Entities.Entities.BookSource
                    {
                        Name = source.Name,
                        Url = source.Url,
                        BookId = item.Id
                    };

                    filmsContext.BookSources.Local.Add(bookSource);
                }
            }
        }

        public static void ConvertBookInPriority(FilmsContext filmsContext, BookInPrioritiesTable destination)
        {
            foreach (BookInPriority item in destination)
            {
                Entities.Entities.BooksInPriority entity = new Entities.Entities.BooksInPriority
                {
                    Id = item.Id,
                    CreationTime = item.CreationTime
                };

                filmsContext.BooksInPriorities.Local.Add(entity);
            }
        }

        public static void ConvertBookTags(FilmsContext filmsContext, BookTagsTable destination)
        {
            foreach (BookTag item in destination)
            {
                Entities.Entities.BookTag entity = new Entities.Entities.BookTag
                {
                    Id = item.Id,
                    Name = item.Name
                };

                filmsContext.BookTags.Local.Add(entity);
            }
        }

        public static void ConvertBooksHaveTags(FilmsContext filmsContext, BookHasTagTable destination)
        {
            foreach (BookHasTag item in destination)
            {
                Entities.Entities.BookTag bookTag = filmsContext.BookTags.Local.First(x => x.Id == item.TagId);
                filmsContext.Books.Local.First(x => x.Id == item.BookId).Tags.Add(bookTag);
            }
        }
    }
}
