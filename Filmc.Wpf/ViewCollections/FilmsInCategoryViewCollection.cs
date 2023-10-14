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
    public class FilmsInCategoryViewCollection : BaseEntityViewCollection
    {
        private readonly int _categoryId;

        public FilmsInCategoryViewCollection(int categoryId, ObservableCollection<FilmViewModel> source)
        {
            _categoryId = categoryId;

            CollectionViewSource.Source = source;
            CollectionViewSource.Filter += OnCollectionFilter;
        }

        private void OnCollectionFilter(object sender, FilterEventArgs e)
        {
            FilmViewModel? vm = e.Item as FilmViewModel;

            if (vm != null)
            {
                e.Accepted = vm.Model.CategoryId == _categoryId;
            }
        }

        protected override IEnumerable<string> GetDescendingProperties()
        {
            return new string[0];
        }
    }
}
