using Filmc.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Recomendations
{
    public class GenreRecomendationsBuilder
    {
        private FilmGenre[]? _genres;
        private double[]? _genresRating;

        private Film[]? _watchedFilms;
        private Film[]? _unwatchedFilms;

        public GenreRecomendationsBuilder()
        {

        }

        public void SetGenres(FilmGenre[] genres)
        {
            _genres = genres;
        }

        public void SetWatchedFilms(Film[] films)
        {
            _watchedFilms = films;
        }

        public void SetUnwatchedFilms(Film[] films)
        {
            _unwatchedFilms = films;
        }

        public void Reset()
        {
            _genres = null;
            _watchedFilms = null;
            _unwatchedFilms = null;
        }

        public void CalculateGenresRaiting()
        {
            if (_watchedFilms == null || _genres == null)
            {
                throw new InvalidOperationException();
            }

            double[] genresRating = new double[_genres.Length];

            for (int i = 0; i < _genres.Length; i++)
                genresRating[i] = 0d;

            var filmsByGenre = _watchedFilms.GroupBy(x => x.Genre);

            foreach (var filmByGenre in filmsByGenre)
            {
                double rawMarkSum = 0;

                foreach (var film in filmByGenre)
                {
                    if (film.Mark.RawMark != null)
                    {
                        rawMarkSum += (int)film.Mark.RawMark;
                    }
                    else
                    {
                        rawMarkSum += 100d;
                    }
                }

                int filmsCount = filmByGenre.Count();
                int genreIndex = Array.IndexOf(_genres, filmByGenre.Key);

                genresRating[genreIndex] = (rawMarkSum / filmsCount) / 300d;
            }

            _genresRating = genresRating;
        }

        public double[] GetRaiting()
        {
            if (_unwatchedFilms == null || _genres == null || _genresRating == null)
            {
                throw new InvalidOperationException();
            }

            double[] filmsRating = new double[_unwatchedFilms.Length];

            for (int filmIndex = 0; filmIndex < _unwatchedFilms.Length; filmIndex++)
            {
                FilmGenre genre = _unwatchedFilms[filmIndex].Genre;
                int genreIndex = Array.IndexOf(_genres, genre);
                filmsRating[filmIndex] = _genresRating[genreIndex];
            }

            return filmsRating;
        }
    }
}
