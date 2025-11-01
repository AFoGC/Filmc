using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Recomendations
{
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

        public override string ToString()
        {
            return $"t:{TagSimilarity}; g:{GenreSimilarity}; c:{CategorySimilarity}; T: {TotalSimilarity}";
        }
    }

    public class ItemSimilarity<T> where T : class
    {
        public ItemSimilarity(T item, Similarity similarity)
        {
            Item = item;
            Similarity = similarity;
        }

        public T Item { get; set; } = null!;
        public Similarity Similarity { get; set; } = null!;
    }
}
