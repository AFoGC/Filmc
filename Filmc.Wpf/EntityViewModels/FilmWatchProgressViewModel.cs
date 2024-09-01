using Filmc.Entities.Entities;
using Filmc.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.EntityViewModels
{
    public class FilmWatchProgressViewModel : BaseViewModel
    {
        private bool _isChecked;

        public FilmWatchProgress Model { get; }

        public FilmWatchProgressViewModel(FilmWatchProgress model)
        {
            Model = model;
            Model.PropertyChanged += OnModelPropertyChanged;

            _isChecked = true;
        }

        private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public string Name
        {
            get => Model.Name;
            set => Model.Name = value;
        }

        public bool IsChecked
        {
            get => _isChecked;
            set { _isChecked = value; OnPropertyChanged(); }
        }
    }
}
