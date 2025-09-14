using Filmc.Entities.Entities;
using Filmc.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Filmc.Wpf.EntityViewModels
{
    public class BookTagCategorySettingsViewModel : BaseViewModel
    {
        private readonly BookTagCategory _model;
        private SolidColorBrush _brush;

        public BookTagCategorySettingsViewModel(BookTagCategory model)
        {
            _model = model;

            var color = new Color();
            color.A = _model.ColorA;
            color.R = _model.ColorR;
            color.G = _model.ColorG;
            color.B = _model.ColorB;
            _brush = new SolidColorBrush(color);

            _model.PropertyChanged += OnModelPropertyChanged;
        }

        public BookTagCategory Model => _model;

        public int Id
        {
            get => _model.Id;
        }

        public string Name
        {
            get => _model.Name;
            set => _model.Name = value;
        }

        public SolidColorBrush Brush
        {
            get => _brush;
            set
            {
                _brush = value;
                _model.ColorA = _brush.Color.A;
                _model.ColorR = _brush.Color.R;
                _model.ColorG = _brush.Color.G;
                _model.ColorB = _brush.Color.B;
            }
        }

        public Color Color
        {
            get => _brush.Color;
            set
            {
                _brush.Color = value;
                _model.ColorA = _brush.Color.A;
                _model.ColorR = _brush.Color.R;
                _model.ColorG = _brush.Color.G;
                _model.ColorB = _brush.Color.B;
            }
        }

        private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);

            if (e.PropertyName == nameof(_model.ColorA) || e.PropertyName == nameof(_model.ColorR) ||
                e.PropertyName == nameof(_model.ColorG) || e.PropertyName == nameof(_model.ColorB))
            {
                OnPropertyChanged(nameof(Brush));
                OnPropertyChanged(nameof(Color));
            }
        }
    }
}
