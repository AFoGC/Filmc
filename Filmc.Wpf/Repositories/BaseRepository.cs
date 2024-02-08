using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Repositories
{
    public class BaseRepository<T> : IEnumerable<T>, ICollection<T>, INotifyCollectionChanged, INotifyPropertyChanged, IBaseRepository where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly ObservableCollection<T> _items;

        public int Count => _items.Count;
        public bool IsReadOnly => false;
        protected DbSet<T> DbSet => _dbSet;

        public BaseRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;

            _dbSet.Load();
            _items = _dbSet.Local.ToObservableCollection();

            foreach (INotifyPropertyChanged item in _items)
                item.PropertyChanged += OnItemPropertyChanged;

            _items.CollectionChanged += OnCollectionChanged;
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action? ItemInCollectionChanged;

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged += OnItemPropertyChanged;
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged += OnItemPropertyChanged;
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (INotifyPropertyChanged item in _items)
                {
                    item.PropertyChanged += OnItemPropertyChanged;
                }
            }
        }

        private void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            ItemInCollectionChanged?.Invoke();
        }

        public virtual void Add(T item)
        {
            _dbSet.Add(item);
        }

        public virtual bool Remove(T item)
        {
            bool exp = false;

            if (_items.Contains(item))
            {
                _dbSet.Remove(item);
                exp = true;
            }

            return exp;
        }

        public virtual void Clear()
        {
            IEnumerable<T> items = _items.ToList();
            _dbSet.RemoveRange(items);
        }

        public virtual bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        protected int GetNewId(Expression<Func<T, int>> selector)
        {
            Func<T, int> func = selector.Compile();

            int max = _items.Max(x => func(x));
            max++;

            return max;
        }
    }
}
