using Filmc.Wpf.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Filmc.Wpf.ViewCollections
{
    public class FilmsViewCollection : BaseEntityViewCollection
    {
        public FilmsViewCollection(ObservableCollection<FilmViewModel> source)
        {
            CollectionViewSource.Source = source;
        }

        protected override IEnumerable<string> GetDescendingProperties()
        {
            yield return "RawMark";
            yield return "RealiseYear";
            yield return "CountOfViews";
            yield return "EndWatchDate";
            yield return "StartWatchDate";
            yield return "WatchedSeries";
            yield return "TotalSeries";
            yield return "Id";
        }
    }
}
