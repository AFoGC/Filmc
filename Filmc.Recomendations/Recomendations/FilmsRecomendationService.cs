using Filmc.Entities.Entities;
using Filmc.Entities.PropertyTypes;
using Filmc.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Recomendations.Recomendations
{
    public class FilmsRecomendationService
    {
        private RepositoriesFacade _repositories;

        private readonly CategoryRecomendationsBuilder _categoryRecomendationsBuilder;
        private readonly GenreRecomendationsBuilder _genreRecomendationsBuilder;
        private readonly TagRecomendationsBuilder _tagRecomendationsBuilder;

        private Film[] _watchedFilms;
        private Film[] _unwatchedFilms;

        private FilmTag[] _tags;
        private FilmGenre[] _genres;
        private FilmCategory[] _categories;

        private double[] _ratingByTags;
        private double[] _ratingByGenres;
        private double[] _ratingByCategories;

        public FilmsRecomendationService()
        {
            _categoryRecomendationsBuilder = new CategoryRecomendationsBuilder();
            _genreRecomendationsBuilder = new GenreRecomendationsBuilder();
            _tagRecomendationsBuilder = new TagRecomendationsBuilder();

            _watchedFilms = new Film[0];
            _unwatchedFilms = new Film[0];

            _tags = new FilmTag[0];
            _genres = new FilmGenre[0];
            _categories = new FilmCategory[0];

            _ratingByTags = new double[0];
            _ratingByGenres = new double[0];
            _ratingByCategories = new double[0];
        }

        public EntityRating<Film>[] CreateRecomendations(RepositoriesFacade repositories)
        {
            _repositories = repositories;

            Refresh();
            CalculateRatingByTags();
            CalculateRatingByGenres();
            CalculateRatingByCategories();

            EntityRating<Film>[] ratings = new EntityRating<Film>[_unwatchedFilms.Length];

            for (int filmIndex = 0; filmIndex < _unwatchedFilms.Length; filmIndex++)
            {
                var film = _unwatchedFilms[filmIndex];
                var rating = new EntityRating<Film>(film)
                {
                    TagRating = _ratingByTags[filmIndex],
                    GenreRating = _ratingByGenres[filmIndex],
                    CategoryRating = _ratingByCategories[filmIndex]
                };

                rating.TotalRating = GetTotalRating(rating);
                ratings[filmIndex] = rating;
            }

            return ratings
                .OrderByDescending(x => x.TotalRating)
                .ToArray();
        }

        private double GetTotalRating(EntityRating<Film> rating)
        {
            double tagModifer = rating.TagRating * 0.75;
            double genreModifer = rating.GenreRating * 0.15;
            double categoryModifer = rating.CategoryRating * 0.05;

            return tagModifer + genreModifer + categoryModifer;
        }

        private void CalculateRatingByTags()
        {
            _tagRecomendationsBuilder.Reset();
            _tagRecomendationsBuilder.SetTags(_tags);
            _tagRecomendationsBuilder.SetWatchedFilms(_watchedFilms);
            _tagRecomendationsBuilder.SetUnwatchedFilms(_unwatchedFilms);

            _tagRecomendationsBuilder.CalculateAvarageProfile();
            _ratingByTags = _tagRecomendationsBuilder.GetRaiting();
        }

        private void CalculateRatingByGenres()
        {
            _genreRecomendationsBuilder.Reset();
            _genreRecomendationsBuilder.SetGenres(_genres);
            _genreRecomendationsBuilder.SetWatchedFilms(_watchedFilms);
            _genreRecomendationsBuilder.SetUnwatchedFilms(_unwatchedFilms);

            _genreRecomendationsBuilder.CalculateGenresRaiting();
            _ratingByGenres = _genreRecomendationsBuilder.GetRaiting();
        }

        private void CalculateRatingByCategories()
        {
            _categoryRecomendationsBuilder.Reset();
            _categoryRecomendationsBuilder.SetCategories(_categories);
            _categoryRecomendationsBuilder.SetWatchedFilms(_watchedFilms);
            _categoryRecomendationsBuilder.SetUnwatchedFilms(_unwatchedFilms);

            _categoryRecomendationsBuilder.CalculateCategoriesRaiting();
            _ratingByCategories = _categoryRecomendationsBuilder.GetRaiting();
        }

        private void Refresh()
        {
            _watchedFilms = GetWatchedFilms();
            _unwatchedFilms = GetNotWatchedFilms();

            _tags = GetTags();
            _genres = GetGenres();
            _categories = GetCategories();
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
