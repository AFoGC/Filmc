using Filmc.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Recomendations.Recomendations
{
    public class TagRecomendationsBuilder
    {
        private FilmTag[]? _tags;
        private int _tagsCount;

        private Film[]? _watchedFilms;
        private double[,]? _watchedFilmsProfiles;

        private Film[]? _unwatchedFilms;
        private double[,]? _unwatchedFilmsProfiles;

        private double[]? _avarageWatchedProfile;

        public TagRecomendationsBuilder()
        {

        }

        public void SetTags(FilmTag[] tags)
        {
            _tags = tags;
            _tagsCount = tags.Length + 1;
        }

        public void SetWatchedFilms(Film[] films)
        {
            _watchedFilms = films;
            _watchedFilmsProfiles = CreateProfilesMatrix(films, true);
        }

        public void SetUnwatchedFilms(Film[] films)
        {
            _unwatchedFilms = films;
            _unwatchedFilmsProfiles = CreateProfilesMatrix(films, false);
        }

        public void Reset()
        {
            _tags = null;
            _tagsCount = 0;

            _watchedFilms = null;
            _watchedFilmsProfiles = null;

            _avarageWatchedProfile = null;

            _unwatchedFilms = null;
            _unwatchedFilmsProfiles = null;
        }

        public void CalculateAvarageProfile()
        {
            if (_tags == null || _watchedFilms == null || _watchedFilmsProfiles == null)
            {
                throw new InvalidOperationException();
            }

            double[] tagVector = new double[_tagsCount];

            for (int tagIndex = 0; tagIndex < _tagsCount; tagIndex++)
            {
                tagVector[tagIndex] = 0;

                for (int filmIndex = 0; filmIndex < _watchedFilms.Length; filmIndex++)
                {
                    tagVector[tagIndex] += _watchedFilmsProfiles[filmIndex, tagIndex];
                }

                tagVector[tagIndex] /= _watchedFilms.Length;
            }

            _avarageWatchedProfile = tagVector;
        }

        public double[] GetRaiting()
        {
            if (_unwatchedFilms == null || _unwatchedFilmsProfiles == null || _avarageWatchedProfile == null)
            {
                throw new InvalidOperationException();
            }

            double[] similarities = new double[_unwatchedFilms.Length];

            for (int filmIndex = 0; filmIndex < _unwatchedFilms.Length; filmIndex++)
            {
                double[] profile = GetOneProfile(_unwatchedFilmsProfiles, filmIndex);
                similarities[filmIndex] = CosSimilarity(profile, _avarageWatchedProfile);
            }

            return similarities;
        }

        private double CosSimilarity(double[] profileA, double[] profileB)
        {
            int count = profileA.GetLength(0);
            double numerator = 0;
            double denominatorA = 0;
            double denominatorB = 0;

            for (int tagIndex = 0; tagIndex < count; tagIndex++)
            {
                numerator += profileA[tagIndex] * profileB[tagIndex];

                denominatorA += Math.Pow(profileA[tagIndex], 2);
                denominatorB += Math.Pow(profileB[tagIndex], 2);
            }

            return numerator / (Math.Sqrt(denominatorA) * Math.Sqrt(denominatorB)); //Cosine Similarity (A, B)
        }

        private double[] GetOneProfile(double[,] profilesMatrix, int filmIndex)
        {
            if (_tags == null)
            {
                throw new InvalidOperationException();
            }

            double[] profile = new double[_tags.Length];

            for (int i = 0; i < _tags.Length; i++)
            {
                profile[i] = profilesMatrix[filmIndex, i];
            }

            return profile;
        }

        private double[,] CreateProfilesMatrix(Film[] films, bool hasMarkInfluencing)
        {
            if (_tags == null)
            {
                throw new InvalidOperationException();
            }

            double[,] profilesMatrix = new double[films.Length, _tagsCount];

            for (int filmIndex = 0; filmIndex < films.Length; filmIndex++)
            {
                Film film = films[filmIndex];
                double tagValue = GetTagValue(film, hasMarkInfluencing);

                if (film.Tags.Count != 0)
                {
                    foreach (FilmTag tag in films[filmIndex].Tags)
                    {
                        int tagIndex = Array.IndexOf(_tags, tag);
                        profilesMatrix[filmIndex, tagIndex] = tagValue;
                    }
                }
                else
                {
                    int noTagIndex = _tagsCount - 1;
                    profilesMatrix[filmIndex, noTagIndex] = tagValue;
                }
            }

            return profilesMatrix;
        }

        private double GetTagValue(Film film, bool hasMarkInfluencing)
        {
            double tagValue = 1;

            if (hasMarkInfluencing == true)
            {
                if (film.Mark.RawMark != null)
                {
                    double rawMark = (double)film.Mark.RawMark;
                    tagValue = rawMark / 300;
                }
                else
                {
                    tagValue = 0.33;
                }
            }

            return tagValue;
        }
    }
}
