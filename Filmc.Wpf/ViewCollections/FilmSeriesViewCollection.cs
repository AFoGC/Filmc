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
    public class FilmSeriesViewCollection : BaseEntityViewCollection
    {
        public FilmSeriesViewCollection(ObservableCollection<FilmViewModel> source)
        {
            CollectionViewSource.Source = source;
            CollectionViewSource.Filter += OnCollectionFilter;

            CollectionViewSource.IsLiveFilteringRequested = true;
            CollectionViewSource.LiveFilteringProperties.Add("Genre.IsSerial");
        }

        private void OnCollectionFilter(object sender, FilterEventArgs e)
        {
            FilmViewModel? vm = e.Item as FilmViewModel;

            if (vm != null)
            {
                if (vm.Model.Genre?.IsSerial == true)
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }

        protected override IEnumerable<string> GetDescendingProperties()
        {
            return new string[0];
        }
    }
}
