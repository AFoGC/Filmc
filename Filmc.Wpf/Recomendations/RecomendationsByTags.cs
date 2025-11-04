using Filmc.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Recomendations
{
    public class RecomendationsByTags
    {
        private readonly Film[] _films;
        private readonly FilmTag[] _tags;
        private readonly double[,] _filmTagProfiles;

        public RecomendationsByTags(Film[] films, FilmTag[] tags)
        {
            _films = films;
            _tags = tags;

            _filmTagProfiles = new double[films.Length, tags.Length];
        }

        public void InitialiseFilmTagProfiles()
        {
            for (int filmIndex = 0; filmIndex < _films.Length; filmIndex++)
            {
                foreach (FilmTag tag in _films[filmIndex].Tags)
                {
                    int tagIndex = Array.IndexOf(_tags, tag);
                    _filmTagProfiles[filmIndex, tagIndex] = 1;
                }
            }
        }

        public double[] CreateAvarageProfile()
        {
            double[] tagVector = new double[_tags.Length];

            for (int tagIndex = 0; tagIndex < _tags.Length; tagIndex++)
            {
                tagVector[tagIndex] = 0;

                for (int filmIndex = 0; filmIndex < _films.Length; filmIndex++)
                {
                    tagVector[tagIndex] += _filmTagProfiles[filmIndex, tagIndex];
                }

                tagVector[tagIndex] /= _films.Length;
            }

            return tagVector;
        }

        public double[] GetSimilaritiesWithProfile(Film[] films, double[] profile)
        {

        }

        public double CosSimilarity(double[] profileA, double[] profileB)
        {
            int count = profileA.GetLength(0);
            double numerator = 0;
            double denominatorA = 0;
            double denominatorB = 0;

            for (int i = 0; i < count; i++)
            {
                numerator += profileA[i] * profileB[i];

                denominatorA += Math.Pow(profileA[i], 2);
                denominatorB += Math.Pow(profileB[i], 2);
            }

            return numerator / (Math.Sqrt(denominatorA) * Math.Sqrt(denominatorB)); //Cosine Similarity (A, B)
        }

        public double[] GetOneProfile(int filmIndex)
        {
            double[] profile = new double[_tags.Length];

            for (int i = 0; i < _tags.Length; i++)
            {
                profile[i] = _filmTagProfiles[filmIndex, i];
            }

            return profile;
        }
    }
}
