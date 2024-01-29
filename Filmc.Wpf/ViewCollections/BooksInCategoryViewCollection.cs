using Filmc.Entities.Entities;
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
    public class BooksInCategoryViewCollection : BaseEntityViewCollection
    {
        private readonly BookCategory _category;

        public BooksInCategoryViewCollection(BookCategory category, ObservableCollection<BookViewModel> source)
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
            BookViewModel? vm = e.Item as BookViewModel;

            if (vm != null)
            {
                e.Accepted = vm.Model.CategoryId == _category.Id;
            }
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
            yield return "Id";
        }
    }
}
