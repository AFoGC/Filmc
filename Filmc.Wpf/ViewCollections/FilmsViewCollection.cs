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
            /*
            yield return "Model.Mark";
            yield return "Model.RealiseYear";
            yield return "Model.CountOfViews";
            yield return "Model.DateOfWatch";
            yield return "Serie.StartWatchDate";
            yield return "Serie.CountOfWatchedSeries";
            yield return "Serie.TotalSeries";
            */
            throw new NotImplementedException();
        }
    }
}
