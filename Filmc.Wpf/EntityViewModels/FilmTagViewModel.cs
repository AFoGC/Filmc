using Filmc.Wpf.ViewModels;
using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.EntityViewModels
{
    public class FilmTagViewModel : BaseViewModel
    {
        private bool _isChecked;

        public FilmTag Model { get; }

        public FilmTagViewModel(FilmTag model)
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
