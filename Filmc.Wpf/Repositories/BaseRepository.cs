using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Repositories
{
    public class BaseRepository<T> : IEnumerable<T>, ICollection<T>, INotifyCollectionChanged, INotifyPropertyChanged where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly ObservableCollection<T> _items;

        public int Count => _items.Count;
        public bool IsReadOnly => false;

        public BaseRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;

            _dbSet.Load();
            _items = _dbSet.Local.ToObservableCollection();

            _items.CollectionChanged += OnCollectionChanged;
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
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
    }
}
