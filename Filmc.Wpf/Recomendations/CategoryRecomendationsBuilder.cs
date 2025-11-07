using Filmc.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Recomendations
{
    public class CategoryRecomendationsBuilder
    {
        private int _categoriesCount;
        private FilmCategory[]? _categories;
        private double[]? _categoriesRating;

        private Film[]? _watchedFilms;
        private Film[]? _unwatchedFilms;

        public CategoryRecomendationsBuilder()
        {
            _categoriesCount = 0;
        }

        public void SetCategories(FilmCategory[] categories)
        {
            _categories = categories;
            _categoriesCount = categories.Length + 1;
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
            _categories = null;
            _categoriesCount = 0;
            _watchedFilms = null;
            _unwatchedFilms = null;
        }

        public void CalculateCategoriesRaiting()
        {
            if (_watchedFilms == null || _categories == null)
            {
                throw new InvalidOperationException();
            }

            double filmsCountWithCategories = GetFilmsCountWithCategories(_watchedFilms);
            double[] categoriesRating = new double[_categoriesCount];

            for (int i = 0; i < _categoriesCount; i++)
                categoriesRating[i] = 0d;

            var filmsByCategory = _watchedFilms.GroupBy(x => x.Genre);

            foreach (var filmByCategory in filmsByCategory)
            {
                double rawMarkSum = 0;

                foreach (var film in filmByCategory)
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

                int filmsCount = filmByCategory.Count();
                int categoryIndex = _categoriesCount - 1;

                double avarageMarkRating = (rawMarkSum / filmsCount) / 300d;
                double countRaiting = 0;

                if (filmByCategory.Key != null)
                {
                    categoryIndex = Array.IndexOf(_categories, filmByCategory.Key);
                    countRaiting = filmsCount / filmsCountWithCategories;
                }

                double categoryRating = avarageMarkRating * 0.9 + countRaiting * 0.1;
                categoriesRating[categoryIndex] = categoryRating;
            }

            _categoriesRating = categoriesRating;
        }

        public double[] GetRaiting()
        {
            if (_unwatchedFilms == null || _categories == null || _categoriesRating == null)
            {
                throw new InvalidOperationException();
            }

            double[] filmsRating = new double[_unwatchedFilms.Length];

            for (int filmIndex = 0; filmIndex < _unwatchedFilms.Length; filmIndex++)
            {
                FilmCategory? category = _unwatchedFilms[filmIndex].Category;

                int categoryIndex = Array.IndexOf(_categories, category);

                if (categoryIndex == -1)
                {
                    categoryIndex = _categoriesCount - 1;
                }

                filmsRating[filmIndex] = _categoriesRating[categoryIndex];
            }

            return filmsRating;
        }

        private int GetFilmsCountWithCategories(Film[] films)
        {
            int count = 0;
            foreach (var film in films)
            {
                if (film.Category != null)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
