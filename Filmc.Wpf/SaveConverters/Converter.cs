using Filmc.Entities.Context;
using Filmc.Wpf.Helper;
using Filmc.Wpf.Services;
using Filmc.Xtl;
using Filmc.Xtl.Entities;
using Filmc.Xtl.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.SaveConverters
{
    public static class Converter
    {
        public static void Convert(Profile profile)
        {
            Convert(profile.Name);
        }

        public static void Convert(string profileName)
        {
            string dirPath = PathHelper.GetProfileDirectoryPath(profileName);
            string xmlPath = Path.Combine(dirPath, "Info.xml");
            string sqlPath = Path.Combine(dirPath, "Info.db");

            if (Directory.Exists(dirPath) == false)
                return;

            if (File.Exists(xmlPath) == false)
                return;

            TablesContext tablesContext = new TablesContext();
            tablesContext.Load(xmlPath);

            FilmsContext filmsContext = CreateContext(sqlPath);

            BooksConverter.ConvertBookCategories(filmsContext, tablesContext.BookCategories);
            BooksConverter.ConvertBookGenres(filmsContext, tablesContext.BookGenres);
            BooksConverter.ConvertBooks(filmsContext, tablesContext.Books);
            BooksConverter.ConvertBookInPriority(filmsContext, tablesContext.BookInPriorities);
            BooksConverter.ConvertBookTags(filmsContext, tablesContext.BookTags);
            BooksConverter.ConvertBooksHaveTags(filmsContext, tablesContext.BookHasTags);

            FilmsConverter.ConvertFilmCategories(filmsContext, tablesContext.FilmCategories);
            FilmsConverter.ConvertFilmGenres(filmsContext, tablesContext.FilmGenres);
            FilmsConverter.ConvertFilms(filmsContext, tablesContext.Films);
            FilmsConverter.ConvertFilmInPriority(filmsContext, tablesContext.FilmInPriorities);
            FilmsConverter.ConvertFilmTags(filmsContext, tablesContext.FilmTags);
            FilmsConverter.ConvertFilmsHaveTags(filmsContext, tablesContext.FilmHasTags);

            filmsContext.SaveChanges();
            PathHelper.ClearSqlitePool(sqlPath);

            string newXmlPath = Path.Combine(dirPath, "Old.xml");
            File.Move(xmlPath, newXmlPath);
        }

        private static FilmsContext CreateContext(string path)
        {
            string connection = "Datasource=" + path;

            var opt = SqliteDbContextOptionsBuilderExtensions.UseSqlite(new DbContextOptionsBuilder(), connection).Options;
            FilmsContext filmsContext = new FilmsContext(opt);
            filmsContext.Database.Migrate();

            return filmsContext;
        }
    }
}
