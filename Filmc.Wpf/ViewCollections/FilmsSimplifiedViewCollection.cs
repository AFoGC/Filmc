﻿using Filmc.Wpf.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        }

        private void OnCollectionFilter(object sender, FilterEventArgs e)
        {
            FilmViewModel? vm = e.Item as FilmViewModel;

            if (vm != null)
            {
                if (vm.Model.CategoryId == null)
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
