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
    public class BooksViewCollection : BaseEntityViewCollection
    {
        public BooksViewCollection(ObservableCollection<BookViewModel> source)
        {
            CollectionViewSource.Source = source;
        }

        protected override IEnumerable<string> GetDescendingProperties()
        {
            yield return "RawMark";
            yield return "PublicationYear";
            yield return "FullReadDate";
            yield return "Author";
            yield return "Bookmark";
            yield return "FullReadDate";
            yield return "CountOfReadings";
        }
    }
}
