using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Filmc.Wpf.ViewCollections
{
    public abstract class BaseEntityViewCollection : IEntityViewCollection
    {
        protected readonly CollectionViewSource CollectionViewSource;
        protected readonly IEnumerable<string> DescendingProperties;

        public BaseEntityViewCollection()
        {
            CollectionViewSource = new CollectionViewSource();
            DescendingProperties = GetDescendingProperties();
        }

        public ICollectionView View => CollectionViewSource.View;

        public void ChangeSortProperty(string propertyName)
        {
            SortDirection direction = SortDirection.Ascending;

            if (DescendingProperties != null)
                if (DescendingProperties.Contains(propertyName))
                    direction = SortDirection.Descending;

            ChangeSortProperty(propertyName, direction);
        }

        protected void ChangeSortProperty(string propertyName, SortDirection direction)
        {
            ListSortDirection sortDirection = (ListSortDirection)direction;

            CollectionViewSource.SortDescriptions.Clear();
            CollectionViewSource.SortDescriptions.Add(new SortDescription(propertyName, sortDirection));
        }

        protected abstract IEnumerable<string> GetDescendingProperties();
    }
}
