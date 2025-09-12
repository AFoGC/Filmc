using Filmc.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Filmc.Wpf.Services
{
    public class BookTagViewMoel
    {
        private readonly BookTag _tag;
        private SolidColorBrush? _brush;

        public BookTagViewMoel(BookTag tag)
        {
            _tag = tag;
        }

        public string Name
        {
            get => _tag.Name;
        }

        public SolidColorBrush? Brush
        {
            get
            {
                if (_brush == null)
                {
                    if (_tag.Category != null)
                    {
                        var category = _tag.Category;
                        Color color = new Color();
                        color.A = category.ColorA;
                        color.R = category.ColorR;
                        color.G = category.ColorG;
                        color.B = category.ColorB;

                        _brush = new SolidColorBrush(color);
                    }
                }

                return _brush;
            }
        }

        public bool HasTag(BookTag tag)
        {
            return _tag == tag;
        }
    }

    public class BookTagsService
    {
        private readonly ObservableCollection<BookTag> _tags;
        private readonly ObservableCollection<BookTagViewMoel> _viewModels;

        public BookTagsService(ObservableCollection<BookTag> tags)
        {
            _tags = tags;
            _viewModels = new ObservableCollection<BookTagViewMoel>();

            _tags.CollectionChanged += OnCollectionChanged;
            foreach (var item in _tags)
            {
                _viewModels.Add(new BookTagViewMoel(item));
            }
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            BookTag tag;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    tag = (BookTag)e.NewItems[0];
                    _viewModels.Add(new BookTagViewMoel(tag));
                    break;

                case NotifyCollectionChangedAction.Remove:
                    tag = (BookTag)e.OldItems[0];
                    var viewModel = _viewModels.First(x => x.HasTag(tag));
                    break;

                case NotifyCollectionChangedAction.Reset:
                    _viewModels.Clear();
                    foreach (var item in _tags)
                    {
                        _viewModels.Add(new BookTagViewMoel(item));
                    }
                    break;

                default:
                    throw new Exception();
            }
        }

        public INotifyCollectionChanged Collection => _viewModels;
    }
}
