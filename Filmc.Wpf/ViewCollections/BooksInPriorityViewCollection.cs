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
    public class BooksInPriorityViewCollection : BaseEntityViewCollection
    {
        public BooksInPriorityViewCollection(ObservableCollection<BookViewModel> source)
        {
            CollectionViewSource.Source = source;
            CollectionViewSource.Filter += OnCollectionFilter;

            CollectionViewSource.IsLiveFilteringRequested = true;
            CollectionViewSource.LiveFilteringProperties.Add("HasPriority");
            CollectionViewSource.LiveFilteringProperties.Add("IsFiltered");
            CollectionViewSource.LiveFilteringProperties.Add("IsFinded");

            CollectionViewSource.IsLiveSortingRequested = true;
            CollectionViewSource.LiveSortingProperties.Add("AddToPriorityTime");

            this.ChangeSortProperty("AddToPriorityTime");
        }

        private void OnCollectionFilter(object sender, FilterEventArgs e)
        {
            BookViewModel? vm = e.Item as BookViewModel;

            if (vm != null)
            {
                bool isAccepted = vm.Model.Priority != null;
                e.Accepted = isAccepted && vm.IsFiltered && vm.IsFinded;
            }
        }

        protected override IEnumerable<string> GetDescendingProperties()
        {
            yield return "AddToPriorityTime";
            yield return "RawMark";
            yield return "PublicationYear";
            yield return "FullReadDate";
            yield return "Author";
            yield return "Bookmark";
            yield return "FullReadDate";
            yield return "CountOfReadings";
            yield return "Id";
        }
    }
}
