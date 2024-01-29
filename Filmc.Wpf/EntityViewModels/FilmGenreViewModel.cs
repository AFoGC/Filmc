using Filmc.Entities.Entities;
using Filmc.Wpf.Commands;
using Filmc.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.EntityViewModels
{
    public class FilmGenreViewModel : BaseViewModel
    {
        private bool _isChecked;

        public FilmGenre Model { get; }

        public FilmGenreViewModel(FilmGenre model)
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

        public bool IsSerial
        {
            get => Model.IsSerial;
            set => Model.IsSerial = value;
        }

        public bool IsChecked
        {
            get => _isChecked;
            set { _isChecked = value; OnPropertyChanged(); }
        }
    }
}
