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
    public class BooksSimplifiedViewCollection : BaseEntityViewCollection
    {
        public BooksSimplifiedViewCollection(ObservableCollection<BookViewModel> source)
        {
            CollectionViewSource.Source = source;
            CollectionViewSource.Filter += OnCollectionFilter;

            CollectionViewSource.IsLiveFilteringRequested = true;
            CollectionViewSource.LiveFilteringProperties.Add("CategoryId");
        }

        private void OnCollectionFilter(object sender, FilterEventArgs e)
        {
            BookViewModel? vm = e.Item as BookViewModel;

            if (vm != null)
            {
                if (vm.Model.CategoryId == 0)
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
