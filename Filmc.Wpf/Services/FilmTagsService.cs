using Filmc.Entities.Entities;
using Filmc.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Filmc.Wpf.Services
{
    public class FilmTagViewMoel : BaseViewModel
    {
        private readonly FilmTag _tag;
        private SolidColorBrush? _brush;
        private FilmTagCategory? _category;

        public FilmTagViewMoel(FilmTag tag)
        {
            _tag = tag;
            _tag.PropertyChanged += OnTagPropertyChanged;

            _category = tag.Category;
            if (_category != null)
            {
                _category.PropertyChanged += OnCategoryPropertyChanged;
            }
        }

        public string Name
        {
            get => _tag.Name;
        }

        public SolidColorBrush? Brush
        {
            get
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
                else
                {
                    _brush = null;
                }

                return _brush;
            }
        }

        public bool HasTag(FilmTag tag)
        {
            return _tag == tag;
        }

        private void OnTagPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_tag.Name))
                OnPropertyChanged(nameof(Name));

            if (e.PropertyName == nameof(_tag.Category))
            {
                if (_category != null)
                    _category.PropertyChanged -= OnCategoryPropertyChanged;

                _category = _tag.Category;

                if (_category != null)
                    _category.PropertyChanged += OnCategoryPropertyChanged;

                OnPropertyChanged(nameof(Brush));
            }
        }

        private void OnCategoryPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Brush));
        }
    }

    public class FilmTagsService
    {
        private readonly ObservableCollection<FilmTag> _tags;
        private readonly ObservableCollection<FilmTagViewMoel> _viewModels;

        public FilmTagsService(ObservableCollection<FilmTag> tags)
        {
            _tags = tags;
            _viewModels = new ObservableCollection<FilmTagViewMoel>();

            _tags.CollectionChanged += OnCollectionChanged;
            foreach (var item in _tags)
            {
                _viewModels.Add(new FilmTagViewMoel(item));
            }
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            FilmTag tag;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    tag = (FilmTag)e.NewItems[0];
                    _viewModels.Add(new FilmTagViewMoel(tag));
                    break;

                case NotifyCollectionChangedAction.Remove:
                    tag = (FilmTag)e.OldItems[0];
                    var viewModel = _viewModels.First(x => x.HasTag(tag));
                    break;

                case NotifyCollectionChangedAction.Reset:
                    _viewModels.Clear();
                    foreach (var item in _tags)
                    {
                        _viewModels.Add(new FilmTagViewMoel(item));
                    }
                    break;

                default:
                    throw new Exception();
            }
        }

        public INotifyCollectionChanged Collection => _viewModels;
    }
}
