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
    public class FilmCategoriesViewCollection : BaseEntityViewCollection
    {
        public FilmCategoriesViewCollection(ObservableCollection<FilmCategoryViewModel> source)
        {
            CollectionViewSource.Source = source;

            CollectionViewSource.Filter += OnCollectionFilter;
            CollectionViewSource.IsLiveFilteringRequested = true;
            CollectionViewSource.LiveFilteringProperties.Add("IsFiltered");
            CollectionViewSource.LiveFilteringProperties.Add("IsFinded");
        }

        private void OnCollectionFilter(object sender, FilterEventArgs e)
        {
            FilmCategoryViewModel? vm = e.Item as FilmCategoryViewModel;

            if (vm != null)
            {
                e.Accepted = vm.IsFiltered && vm.IsFinded;
            }
        }

        protected override IEnumerable<string> GetDescendingProperties()
        {
            yield return "RawMark";
            yield return "Id";
        }
    }
}
