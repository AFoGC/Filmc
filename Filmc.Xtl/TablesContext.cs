using Filmc.Xtl.Tables;
using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl
{
    public class TablesContext : TablesCollection
    {
        public TablesContext()
        {
            Configure(builder =>
            {
                builder.AddTable<FilmsTable, Film>(tb =>
                {
                    tb.DefaultRecord = new Film();

                    tb.SetIdGeneration(id => ++id);
                    tb.AddTableSaveRule(x => x.MarkSystem, 6);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.Name);
                    tb.AddEntitySaveRule(x => x.GenreId);
                    tb.AddEntitySaveRule(x => x.RealiseYear);
                    tb.AddEntitySaveRule(x => x.IsWatched);
                    tb.AddEntitySaveRule(x => x.EndWatchDate);
                    tb.AddEntitySaveRule(x => x.Comment);
                    tb.AddEntitySaveRule(x => x.CountOfViews);
                    tb.AddEntitySaveRule(x => x.CategoryId);
                    tb.AddEntitySaveRule(x => x.CategoryListId);
                    tb.AddEntitySaveRule(x => x.StartWatchDate);
                    tb.AddEntitySaveRule(x => x.WatchedSeries);
                    tb.AddEntitySaveRule(x => x.TotalSeries);
                    tb.AddEntitySaveRule(x => x.RawMark);
                    tb.AddEntitySaveRule(x => x.Sources);
                });

                builder.AddTable<FilmCategoriesTable, FilmCategory>(tb =>
                {
                    tb.DefaultRecord = new FilmCategory();

                    tb.SetIdGeneration(id => ++id);
                    tb.AddTableSaveRule(x => x.MarkSystem, 6);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.Name);
                    tb.AddEntitySaveRule(x => x.HideName);
                    tb.AddEntitySaveRule(x => x.RawMark);
                    tb.AddEntitySaveRule(x => x.Priority);
                });

                builder.AddTable<FilmGenresTable, FilmGenre>(tb =>
                {
                    tb.DefaultRecord = new FilmGenre();

                    tb.SetIdGeneration(id => ++id);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.Name);
                    tb.AddEntitySaveRule(x => x.IsSerial);
                });

                builder.AddTable<FilmHasTagTable, FilmHasTag>(tb =>
                {
                    tb.DefaultRecord = new FilmHasTag();

                    tb.SetIdGeneration(id => ++id);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.FilmId);
                    tb.AddEntitySaveRule(x => x.TagId);
                });

                builder.AddTable<FilmInPrioritiesTable, FilmInPriority>(tb =>
                {
                    tb.DefaultRecord = new FilmInPriority();

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.CreationTime);
                });

                builder.AddTable<FilmTagsTable, FilmTag>(tb =>
                {
                    tb.DefaultRecord = new FilmTag();

                    tb.SetIdGeneration(id => ++id);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.Name);
                });

                builder.AddTable<BooksTable, Book>(tb =>
                {
                    tb.DefaultRecord = new Book();

                    tb.SetIdGeneration(id => ++id);
                    tb.AddTableSaveRule(x => x.MarkSystem, 6);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.Name);
                    tb.AddEntitySaveRule(x => x.Author);
                    tb.AddEntitySaveRule(x => x.GenreId);
                    tb.AddEntitySaveRule(x => x.PublicationYear);
                    tb.AddEntitySaveRule(x => x.IsReaded);
                    tb.AddEntitySaveRule(x => x.FullReadDate);
                    tb.AddEntitySaveRule(x => x.CountOfReadings);
                    tb.AddEntitySaveRule(x => x.Bookmark);
                    tb.AddEntitySaveRule(x => x.CategoryId);
                    tb.AddEntitySaveRule(x => x.CategoryListId);
                    tb.AddEntitySaveRule(x => x.RawMark);
                    tb.AddEntitySaveRule(x => x.Sources);
                });

                builder.AddTable<BookCategoriesTable, BookCategory>(tb =>
                {
                    tb.DefaultRecord = new BookCategory();

                    tb.SetIdGeneration(id => ++id);
                    tb.AddTableSaveRule(x => x.MarkSystem, 6);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.Name);
                    tb.AddEntitySaveRule(x => x.HideName);
                    tb.AddEntitySaveRule(x => x.RawMark);
                    tb.AddEntitySaveRule(x => x.Priority);
                });

                builder.AddTable<BookGenresTable, BookGenre>(tb =>
                {
                    tb.DefaultRecord = new BookGenre();

                    tb.SetIdGeneration(id => ++id);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.Name);
                });

                builder.AddTable<BookHasTagTable, BookHasTag>(tb =>
                {
                    tb.DefaultRecord = new BookHasTag();

                    tb.SetIdGeneration(id => ++id);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.BookId);
                    tb.AddEntitySaveRule(x => x.TagId);
                });

                builder.AddTable<BookInPrioritiesTable, BookInPriority>(tb =>
                {
                    tb.DefaultRecord = new BookInPriority();

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.CreationTime);
                });

                builder.AddTable<BookTagsTable, BookTag>(tb =>
                {
                    tb.DefaultRecord = new BookTag();

                    tb.SetIdGeneration(id => ++id);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.Name);
                });

                builder.AddOneToMany<FilmGenre, Film>(x => x.GenreId, x => x.Genre, x => x.Films);
                builder.AddOneToMany<FilmCategory, Film>(x => x.CategoryId, x => x.Category, x => x.Films);
                builder.AddOneToOne<FilmInPriority, Film>(x => x.Film, y => y.Priority);
                builder.AddOneToMany<Film, FilmHasTag>(x => x.FilmId, x => x.Film, x => x.HasTags);
                builder.AddOneToMany<FilmTag, FilmHasTag>(x => x.TagId, x => x.Tag, x => x.HasFilms);

                builder.AddOneToMany<BookGenre, Book>(x => x.GenreId, x => x.Genre, x => x.Books);
                builder.AddOneToMany<BookCategory, Book>(x => x.CategoryId, x => x.Category, x => x.Books);
                builder.AddOneToOne<BookInPriority, Book>(x => x.Book, y => y.Priority);
                builder.AddOneToMany<Book, BookHasTag>(x => x.BookId, x => x.Book, x => x.HasTags);
                builder.AddOneToMany<BookTag, BookHasTag>(x => x.TagId, x => x.Tag, x => x.HasBooks);
            });
        }

        public FilmsTable Films => GetTable<FilmsTable>();
        public FilmCategoriesTable FilmCategories => GetTable<FilmCategoriesTable>();
        public FilmGenresTable FilmGenres => GetTable<FilmGenresTable>();
        public FilmHasTagTable FilmHasTags => GetTable<FilmHasTagTable>();
        public FilmInPrioritiesTable FilmInPriorities => GetTable<FilmInPrioritiesTable>();
        public FilmTagsTable FilmTags => GetTable<FilmTagsTable>();

        public BooksTable Books => GetTable<BooksTable>();
        public BookCategoriesTable BookCategories => GetTable<BookCategoriesTable>();
        public BookGenresTable BookGenres => GetTable<BookGenresTable>();
        public BookHasTagTable BookHasTags => GetTable<BookHasTagTable>();
        public BookInPrioritiesTable BookInPriorities => GetTable<BookInPrioritiesTable>();
        public BookTagsTable BookTags => GetTable<BookTagsTable>();

    }
}
