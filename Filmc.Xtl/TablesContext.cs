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

                    tb.SetIdGeneration(id => id++);
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

                    tb.SetIdGeneration(id => id++);
                    tb.AddTableSaveRule(x => x.MarkSystem, 6);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.Name);
                    tb.AddEntitySaveRule(x => x.HideName);
                    tb.AddEntitySaveRule(x => x.Priority);
                });

                builder.AddTable<FilmGenresTable, FilmGenre>(tb =>
                {
                    tb.DefaultRecord = new FilmGenre();

                    tb.SetIdGeneration(id => id++);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.Name);
                    tb.AddEntitySaveRule(x => x.IsSerial);
                });

                builder.AddTable<FilmHasTagTable, FilmHasTag>(tb =>
                {
                    tb.DefaultRecord = new FilmHasTag();

                    tb.SetIdGeneration(id => id++);

                    tb.SetEntityId(x => x.Id);
                    tb.AddEntitySaveRule(x => x.FilmId);
                    tb.AddEntitySaveRule(x => x.TagId);
                });
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
