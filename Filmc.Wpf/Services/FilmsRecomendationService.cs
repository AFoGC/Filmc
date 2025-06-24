using Filmc.Entities.Entities;
using Filmc.Wpf.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Services
{
    public class FilmsRecomendationService
    {
        private readonly RepositoriesFacade _repositories;

        public FilmsRecomendationService(RepositoriesFacade repositories)
        {
            _repositories = repositories;
        }

        public void CreateRecomendations()
        {
            var watched = GetWatchedFilms();
            GetUserProfile(watched);



        }

        public IEnumerable<Film> GetWatchedFilms()
        {
            var watched = _repositories.FilmProgresses.Last();
            return _repositories.Films.Where(x => x.WatchProgress == watched);
        }

        public IEnumerable<Film> GetNotWatchedFilms()
        {
            var watched = _repositories.FilmProgresses.First();
            return _repositories.Films.Where(x => x.WatchProgress == watched);
        }

        public void GetUserProfile(IEnumerable<Film> films)
        {
            Dictionary<FilmTag, RecomendationWeight> tagWeights = new Dictionary<FilmTag, RecomendationWeight>();
            Dictionary<FilmGenre, RecomendationWeight> genreWeights = new Dictionary<FilmGenre, RecomendationWeight>(); 
            Dictionary<FilmCategory, RecomendationWeight> categoryWeights = new Dictionary<FilmCategory, RecomendationWeight>();

            foreach (var tag in _repositories.FilmTags)
            {
                tagWeights[tag] = new RecomendationWeight();
            }

            foreach (var genre in _repositories.FilmGenres)
            {
                genreWeights[genre] = new RecomendationWeight();
            }

            foreach (var tag in _repositories.FilmTags)
            {
                tagWeights[tag] = new RecomendationWeight();
            }

            foreach (var film in films)
            {
                int rawMark = -1;

                if (film.Mark.RawMark != null)
                {
                    rawMark = film.Mark.RawMark.Value;
                }

                if (rawMark != -1)
                {
                    foreach (var tag in film.Tags)
                    {
                        var weight = tagWeights[tag];

                        weight.Count += 1;
                        weight.MarkSum += rawMark;
                    }

                    genreWeights[film.Genre].Count += 1;
                    genreWeights[film.Genre].MarkSum += rawMark;

                    if (film.Category != null)
                    {
                        categoryWeights[film.Category].Count += 1;
                        categoryWeights[film.Category].MarkSum += rawMark;
                    }
                }
            }


        }

        private class RecomendationWeight
        {
            public RecomendationWeight()
            {
                Count = 0;
                MarkSum = 0;
            }

            public int Count { get; set; }
            public int MarkSum { get; set; }

            public int GetMidMark()
            {
                return MarkSum / Count;
            }
        }
    }
}
