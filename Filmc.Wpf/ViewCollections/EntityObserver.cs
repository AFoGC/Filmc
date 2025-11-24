using Filmc.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.ViewCollections
{
    public class EntityObserver<TEntity, TViewModel> where TEntity : class
    {
        private readonly ObservableCollection<TViewModel> _viewModels;
        private readonly Func<TEntity, TViewModel> CreateViewModelAction;

        private BaseRepository<TEntity>? _source;
        

        public EntityObserver(ObservableCollection<TViewModel> viewModels, Func<TEntity, TViewModel> createViewModelAction)
        {
            _viewModels = viewModels;
            CreateViewModelAction = createViewModelAction;
        }

        public void SetSource(BaseRepository<TEntity> source)
        {
            if (_source != null)
                _source.CollectionChanged -= OnSourceCollectionChanged;

            _source = source;
            Reset();

            _source.CollectionChanged += OnSourceCollectionChanged;
        }

        private void OnSourceCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Add(e);
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                Remove(e);
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Reset();
            }
        }

        private void Add(NotifyCollectionChangedEventArgs e)
        {
            TEntity entity = (TEntity)e.NewItems[0]!;
            TViewModel viewModel = CreateViewModelAction(entity);

            _viewModels.Insert(e.NewStartingIndex, viewModel);
        }

        private void Remove(NotifyCollectionChangedEventArgs e)
        {
            int i = e.OldStartingIndex;
            _viewModels.RemoveAt(i);
        }

        private void Reset()
        {
            _viewModels.Clear();

            if (_source != null)
            {
                foreach (TEntity entity in _source)
                {
                    TViewModel viewModel = CreateViewModelAction(entity);
                    _viewModels.Add(viewModel);
                }
            }
        }
    }
}
