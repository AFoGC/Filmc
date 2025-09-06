using Filmc.Wpf.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Filmc.Wpf.ViewCollections
{
    public class FilmsSimplifiedViewCollection : BaseEntityViewCollection
    {
        public FilmsSimplifiedViewCollection(ObservableCollection<FilmViewModel> source)
        {
            CollectionViewSource.Source = source;
            CollectionViewSource.Filter += OnCollectionFilter;

            CollectionViewSource.IsLiveFilteringRequested = true;
            CollectionViewSource.LiveFilteringProperties.Add("CategoryId");
            //CollectionViewSource.LiveSortingProperties.Add("CategoryListId");

            CollectionViewSource.GroupDescriptions.Clear();
            CollectionViewSource.IsLiveSortingRequested = true;
            var group = new PropertyGroupDescription("Model.Category");
            group.SortDescriptions.Add(new SortDescription("Name.Id", ListSortDirection.Descending));
            CollectionViewSource.GroupDescriptions.Add(group);
        }

        private void OnCollectionFilter(object sender, FilterEventArgs e)
        {
            FilmViewModel? vm = e.Item as FilmViewModel;

            if (vm != null)
            {
                if (vm.Model.CategoryId == null)
                {
                    e.Accepted = false;
                }
                else
                {
                    e.Accepted = true;
                }
            }
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
