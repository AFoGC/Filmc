using Filmc.Entities.Entities;
using Filmc.Wpf.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Filmc.Wpf.ViewCollections
{
    public class FilmsInCategoryViewCollection : BaseEntityViewCollection
    {
        private readonly FilmCategory _category;

        public FilmsInCategoryViewCollection(FilmCategory category, ObservableCollection<FilmViewModel> source)
        {
            _category = category;

            CollectionViewSource.Source = source;
            CollectionViewSource.Filter += OnCollectionFilter;

            CollectionViewSource.IsLiveFilteringRequested = true;
            CollectionViewSource.LiveFilteringProperties.Add("CategoryId");

            CollectionViewSource.IsLiveSortingRequested = true;
            CollectionViewSource.LiveSortingProperties.Add("CategoryListId");

            ChangeSortProperty("CategoryListId");

        }

        private void OnCollectionFilter(object sender, FilterEventArgs e)
        {
            FilmViewModel? vm = e.Item as FilmViewModel;

            if (vm != null)
            {
                e.Accepted = vm.Model.CategoryId == _category.Id;
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
