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
    public static class FilmsConverter
    {
        public static void ConvertFilmCategories(FilmsContext filmsContext, FilmCategoriesTable destination)
        {
            foreach (FilmCategory item in destination)
            {
                Entities.Entities.FilmCategory entity = new Entities.Entities.FilmCategory
                {
                    Id = item.Id,
                    Name = item.Name,
                    HideName = item.HideName
                };

                entity.Mark.RawMark = item.Mark.RawMark;

                filmsContext.FilmCategories.Local.Add(entity);
            }
        }

        public static void ConvertFilmGenres(FilmsContext filmsContext, FilmGenresTable destination)
        {
            foreach (FilmGenre item in destination)
            {
                Entities.Entities.FilmGenre entity = new Entities.Entities.FilmGenre
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsSerial = item.IsSerial
                };

                filmsContext.FilmGenres.Local.Add(entity);
            }
        }

        public static void ConvertFilms(FilmsContext filmsContext, FilmsTable destination)
        {
            foreach (Film item in destination)
            {
                Entities.Entities.Film entity = new Entities.Entities.Film
                {
                    Id = item.Id,
                    Name = item.Name,
                    GenreId = item.GenreId
                };

                if (item.RealiseYear != 0)
                    entity.RealiseYear = item.RealiseYear;

                entity.IsWatched = item.IsWatched;
                entity.EndWatchDate = item.EndWatchDate;
                entity.Comment = item.Comment;

                if (item.CountOfViews != 0)
                    entity.CountOfViews = item.CountOfViews;

                entity.StartWatchDate = item.StartWatchDate;

                if (item.WatchedSeries != 0)
                    entity.WatchedSeries = item.WatchedSeries;

                if (item.TotalSeries != 0)
                    entity.TotalSeries = item.TotalSeries;

                entity.Mark.RawMark = item.Mark.RawMark;

                filmsContext.Films.Local.Add(entity);

                if (item.CategoryId != 0)
                    filmsContext.FilmCategories.Local
                        .First(x => x.Id == item.CategoryId)
                        .AddFilmInOrder(entity);

                foreach (var source in item.Sources)
                {
                    Entities.Entities.FilmSource filmSource = new Entities.Entities.FilmSource
                    {
                        Name = source.Name,
                        Url = source.Url,
                        FilmId = item.Id
                    };

                    filmsContext.FilmSources.Local.Add(filmSource);
                }
            }
        }

        public static void ConvertFilmInPriority(FilmsContext filmsContext, FilmInPrioritiesTable destination)
        {
            foreach (FilmInPriority item in destination)
            {
                Entities.Entities.FilmsInPriority entity = new Entities.Entities.FilmsInPriority
                {
                    Id = item.Id,
                    CreationTime = item.CreationTime
                };

                filmsContext.FilmsInPriorities.Local.Add(entity);
            }
        }

        public static void ConvertFilmTags(FilmsContext filmsContext, FilmTagsTable destination)
        {
            foreach (FilmTag item in destination)
            {
                Entities.Entities.FilmTag entity = new Entities.Entities.FilmTag
                {
                    Id = item.Id,
                    Name = item.Name
                };

                filmsContext.FilmTags.Local.Add(entity);
            }
        }

        public static void ConvertFilmsHaveTags(FilmsContext filmsContext, FilmHasTagTable destination)
        {
            foreach (FilmHasTag item in destination)
            {
                Entities.Entities.FilmTag filmTag = filmsContext.FilmTags.Local.First(x => x.Id == item.TagId);
                filmsContext.Films.Local.First(x => x.Id == item.FilmId).Tags.Add(filmTag);
            }
        }
    }
}
