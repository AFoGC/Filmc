using Filmc.Entities.Entities;
using Filmc.Entities.PropertyTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Recomendations
{
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
}
