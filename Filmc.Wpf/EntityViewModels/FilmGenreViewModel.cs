using Filmc.Wpf.Commands;
using Filmc.Wpf.ViewModels;
using Filmc.Xtl.Entities;
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
        private bool _isChangeSerialEnable;

        public FilmGenre Model { get; }

        public FilmGenreViewModel(FilmGenre model)
        {
            Model = model;
            Model.PropertyChanged += OnModelPropertyChanged;

            _isChecked = true;
            _isChangeSerialEnable = model.Films.Count == 0;

            model.Films.CollectionChanged += OnFilmsCollectionChanged;
        }

        private void OnFilmsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            IsChangeSerialEnable = Model.Films.Count == 0;
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

        public bool IsChangeSerialEnable
        {
            get => _isChangeSerialEnable;
            set { _isChangeSerialEnable = value; OnPropertyChanged(); }
        }
    }
}
