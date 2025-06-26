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

        public void CreateRecomendations()
        {
            Film[] watchedFilms = GetWatchedFilms();
            FilmTag[] tags = GetTags();

            VectorMatrix vectorMatrix = new VectorMatrix();
            vectorMatrix.CalculateAvarageProfile(watchedFilms, tags);
        }

        public Film[] GetWatchedFilms()
        {
            var watched = _repositories.FilmProgresses.Last();
             return _repositories.Films
                .Where(x => x.WatchProgress == watched)
                .ToArray();
        }

        public Film[] GetNotWatchedFilms()
        {
            var watched = _repositories.FilmProgresses.First();
            return _repositories.Films
                .Where(x => x.WatchProgress == watched)
                .ToArray();
        }

        public FilmTag[] GetTags()
        {
            return _repositories.FilmTags.ToArray();
        }
    }

    public class VectorMatrix
    {
        public double[] AvarageTagProfile { get; private set; }
 
        public VectorMatrix()
        {
            AvarageTagProfile = new double[0];
        }

        public void CalculateAvarageProfile(Film[] filmsArr, FilmTag[] tagsArr)
        {
            double[,] tagMatrix = CreateTagMatrix(filmsArr, tagsArr);
            AvarageTagProfile = CalculateAvarageTagProfile(tagMatrix);


        }

        private double[,] CreateTagMatrix(Film[] filmsArr, FilmTag[] tagsArr)
        {
            int filmsCount = filmsArr.Length;
            int tagsCount = tagsArr.Length;
            double[,] matrix = new double[filmsCount, tagsCount];

            for (int filmIndex = 0; filmIndex < filmsCount; filmIndex++)
            {
                if (filmsArr[filmIndex].Mark.RawMark != null)
                {
                    double rawMark = (double)filmsArr[filmIndex].Mark.RawMark;

                    foreach (FilmTag tag in filmsArr[filmIndex].Tags)
                    {
                        int tagIndex = Array.IndexOf(tagsArr, tag);
                        matrix[filmIndex, tagIndex] = rawMark / Mark.MaxRawMark;
                    }
                }
            }

            return matrix;
        }

        private double[] CalculateAvarageTagProfile(double[,] tagMatrix)
        {
            double sum = 0;
            int filmsCount = tagMatrix.GetLength(0);
            int tagsCount = tagMatrix.GetLength(1);
            double[] avarageTagProfile = new double[tagsCount];

            for (int tagIndex = 0; tagIndex < tagsCount; tagIndex++)
            {
                for (int filmIndex = 0; filmIndex < filmsCount; filmIndex++)
                {
                    sum += tagMatrix[filmIndex, tagIndex];
                }

                avarageTagProfile[tagIndex] = sum / filmsCount;
                sum = 0;
            }

            return avarageTagProfile;
        }
    }
}
