using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Recomendations
{
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
}
