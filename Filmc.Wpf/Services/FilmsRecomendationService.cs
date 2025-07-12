using Filmc.Entities.Entities;
using Filmc.Entities.PropertyTypes;
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

        public Tuple<Film, Similarity>[] CreateRecomendations()
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

            Tuple<Film, Similarity>[] recomendations = new Tuple<Film, Similarity>[notWatchedFilms.Length];

            for (int i = 0; i < notWatchedFilms.Length; i++)
            {
                recomendations[i] = new Tuple<Film, Similarity>(notWatchedFilms[i], similarities[i]);
            }

            return recomendations
                .OrderByDescending(x => x.Item2.TotalSimilarity)
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

    public class FilmsMatrix
    {
        public Film[] Films { get; private set; }
        public FilmTag[] Tags { get; private set; }
        public FilmGenre[] Genres { get; private set; }
        public FilmCategory[] Categories { get; private set; }
        public FilmProfile[] Profiles { get; private set; }

        public int TagsVectorSize => Tags.Length + 1;
        public int GenresVectorSize => Genres.Length;
        public int CategoriesVectorSize => Categories.Length + 1;

        public FilmsMatrix()
        {
            Films = new Film[0];
            Tags = new FilmTag[0];
            Genres = new FilmGenre[0];
            Categories = new FilmCategory[0];
            Profiles = new FilmProfile[0];
        }

        public void CreateProfiles(Film[] films, FilmTag[] tags, FilmGenre[] genres, FilmCategory[] categories, bool isMarkCounting)
        {
            Films = films;
            Tags = tags;
            Genres = genres;
            Categories = categories;

            int filmsCount = films.Length;
            int tagsCount = TagsVectorSize;
            int genresCount = GenresVectorSize;
            int categoriesCount = CategoriesVectorSize;

            FilmProfile[] profiles = new FilmProfile[filmsCount];
            Profiles = profiles;

            for (int filmIndex = 0; filmIndex < filmsCount; filmIndex++)
            {
                FilmProfile profile = new FilmProfile(tagsCount, genresCount, categoriesCount);
                profiles[filmIndex] = profile;

                Film film = films[filmIndex];
                double vectorDirectionValue = 1;

                if (isMarkCounting)
                    vectorDirectionValue = (double)film.Mark.RawMark / Mark.MaxRawMark;

                if (film.Tags.Count != 0)
                {
                    foreach (FilmTag tag in film.Tags)
                    {
                        int tagIndex = Array.IndexOf(tags, tag);
                        profile.TagVectors[tagIndex] = vectorDirectionValue;
                    }
                }
                else
                {
                    profile.TagVectors[tagsCount - 1] = vectorDirectionValue;
                }
                

                if (film.Category != null)
                {
                    int categotyIndex = Array.IndexOf(categories, film.Category);
                    profile.CategoryVectors[categotyIndex] = vectorDirectionValue;
                }
                else
                {
                    profile.CategoryVectors[categoriesCount - 1] = vectorDirectionValue;
                }

                int genreIndex = Array.IndexOf(genres, film.Genre);
                profile.GenreVectors[genreIndex] = vectorDirectionValue;
            }
        }

        public FilmProfile CreateAvarageProfile()
        {
            int filmsCount = Films.Length;
            int tagsCount = TagsVectorSize;
            int genresCount = GenresVectorSize;
            int categoriesCount = CategoriesVectorSize;
            double sum = 0;

            FilmProfile profile = new FilmProfile(tagsCount, genresCount, categoriesCount);

            for (int tagIndex = 0; tagIndex < tagsCount; tagIndex++)
            {
                for (int filmIndex = 0; filmIndex < filmsCount; filmIndex++)
                {
                    sum += Profiles[filmIndex].TagVectors[tagIndex];
                }

                profile.TagVectors[tagIndex] = sum / filmsCount;
                sum = 0;
            }

            for (int genreIndex = 0; genreIndex < genresCount; genreIndex++)
            {
                for (int filmIndex = 0; filmIndex < filmsCount; filmIndex++)
                {
                    sum += Profiles[filmIndex].GenreVectors[genreIndex];
                }

                profile.GenreVectors[genreIndex] = sum / filmsCount;
                sum = 0;
            }

            for (int categoryIndex = 0; categoryIndex < categoriesCount; categoryIndex++)
            {
                for (int filmIndex = 0; filmIndex < filmsCount; filmIndex++)
                {
                    sum += Profiles[filmIndex].CategoryVectors[categoryIndex];
                }

                profile.CategoryVectors[categoryIndex] = sum / filmsCount;
                sum = 0;
            }

            return profile;
        }

        public Similarity[] GetSimilarities(FilmProfile avarage)
        {
            int filmsCount = Films.Length;
            Similarity[] similarities = new Similarity[filmsCount];

            for (int i = 0; i < filmsCount; i++)
            {
                similarities[i] = GetSimilarity(avarage, Profiles[i]);
            }

            return similarities;
        }

        public Similarity GetSimilarity(FilmProfile avarage, FilmProfile profile)
        {
            double tagSimilarity = CosSimilarity(avarage.TagVectors, profile.TagVectors);
            double genreSimilarity = CosSimilarity(avarage.GenreVectors, profile.GenreVectors);
            double categorySimilarity = CosSimilarity(avarage.CategoryVectors, profile.CategoryVectors);

            return new Similarity(tagSimilarity, genreSimilarity, categorySimilarity);
        }

        public double CosSimilarity(double[] avarage, double[] profile)
        {
            int count = avarage.GetLength(0);
            double numerator = 0;
            double denominatorLeft = 0;
            double denominatorRight = 0;

            for (int i = 0; i < count; i++)
            {
                numerator += avarage[i] * profile[i];

                denominatorLeft += Math.Pow(avarage[i], 2);
                denominatorRight += Math.Pow(profile[i], 2);
            }

            return numerator / (Math.Sqrt(denominatorLeft) * Math.Sqrt(denominatorRight)); //Cosine Similarity (A, B)
        }
    }

    public class FilmProfile
    {
        public double[] TagVectors { get; private set; }
        public double[] GenreVectors { get; private set; }
        public double[] CategoryVectors { get; private set; }

        public FilmProfile(int tagsCount, int genresCount, int categoryCount)
        {
            TagVectors = new double[tagsCount];
            GenreVectors = new double[genresCount];
            CategoryVectors = new double[categoryCount];
        }
    }

    public class Similarity
    {
        public double TagSimilarity { get; }
        public double GenreSimilarity { get; }
        public double CategorySimilarity { get; }

        public double TotalSimilarity { get; }

        public Similarity(double tagSimilarity, double genreSimilarity, double categorySimilarity)
        {
            TagSimilarity = tagSimilarity;
            GenreSimilarity = genreSimilarity;
            CategorySimilarity = categorySimilarity;

            TotalSimilarity = 
                tagSimilarity * 0.75 + 
                genreSimilarity * 0.15 + 
                categorySimilarity * 0.05;
        }
    }
}
