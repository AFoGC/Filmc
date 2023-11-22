using Filmc.Wpf.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.ViewCollections
{
    public class FilmCategoriesViewCollection : BaseEntityViewCollection
    {
        public FilmCategoriesViewCollection(ObservableCollection<FilmCategoryViewModel> source)
        {
            CollectionViewSource.Source = source;
        }

        protected override IEnumerable<string> GetDescendingProperties()
        {
            yield return "RawMark";
            yield return "Id";
        }
    }
}
