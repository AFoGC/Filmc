using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Recomendations
{
    public class EntityRating<T>
    {
        public EntityRating(T entity)
        {
            Entity = entity;
        }

        public T Entity { get; }

        public double TagRating { get; set; }
        public double GenreRating { get; set; }
        public double CategoryRating { get; set; }
        public double TotalRating { get; set; }
    }
}
