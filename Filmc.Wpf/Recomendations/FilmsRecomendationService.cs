using Filmc.Entities.Entities;
using Filmc.Entities.PropertyTypes;
using Filmc.Wpf.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Recomendations
{
    public class FilmsRecomendationService
    {
        private readonly RepositoriesFacade _repositories;

        public FilmsRecomendationService(RepositoriesFacade repositories)
        {
            _repositories = repositories;
        }

        public ItemSimilarity<Film>[] CreateRecomendations()
        {
            Film[] watchedFilms = GetWatchedFilms();
            Film[] notWatchedFilms = GetNotWatchedFilms();

            FilmTag[] tags = GetTags();
            FilmGenre[] genres = GetGenres();
            FilmCategory[] categories = GetCategories();

            FilmsMatrix watchedMatrix = new FilmsMatrix();
            FilmsMatrix notWatchedMatrix = new FilmsMatrix();

            watchedMatrix.CreateProfiles(watchedFilms, tags, genres, categories, true);
            notWatchedMatrix.CreateProfiles(notWatchedFilms, tags, genres, categories, false);

            FilmProfile avarageProfile = watchedMatrix.CreateAvarageProfile();
            Similarity[] similarities = notWatchedMatrix.GetSimilarities(avarageProfile);

            ItemSimilarity<Film>[] recomendations = new ItemSimilarity<Film>[notWatchedFilms.Length];

            for (int i = 0; i < notWatchedFilms.Length; i++)
            {
                recomendations[i] = new ItemSimilarity<Film>(notWatchedFilms[i], similarities[i]);
            }

            return recomendations
                .OrderByDescending(x => x.Similarity.TotalSimilarity)
                .ToArray();
        }

        private Film[] GetWatchedFilms()
        {
            var watched = _repositories.FilmProgresses.Last();
            return _repositories.Films
               .Where(x => x.WatchProgress == watched)
               .Where(x => x.Mark.RawMark != null)
               .ToArray();
        }

        private Film[] GetNotWatchedFilms()
        {
            var watched = _repositories.FilmProgresses.First();
            return _repositories.Films
                .Where(x => x.WatchProgress == watched)
                .ToArray();
        }

        private FilmTag[] GetTags()
        {
            return _repositories.FilmTags.ToArray();
        }

        private FilmGenre[] GetGenres()
        {
            return _repositories.FilmGenres.ToArray();
        }

        private FilmCategory[] GetCategories()
        {
            return _repositories.FilmCategories.ToArray();
        }
    }
}
