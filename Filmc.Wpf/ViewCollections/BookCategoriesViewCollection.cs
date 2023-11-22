using Filmc.Wpf.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Filmc.Wpf.ViewCollections
{
    public class BookCategoriesViewCollection : BaseEntityViewCollection
    {
        public BookCategoriesViewCollection(ObservableCollection<BookCategoryViewModel> source)
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
