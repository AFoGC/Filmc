using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Filmc.Entities.Entities
{
    public partial class FilmGenre : BaseEntity
    {
        private int _id;
        private string _name = null!;
        private bool _isSerial;

        public FilmGenre()
        {
            Name = String.Empty;

            Films = new ObservableCollection<Film>();
        }

        public int Id 
        { 
            get => _id; 
            set { _id = value; OnPropertyChanged(); }
        }
        public string Name 
        { 
            get => _name; 
            set { _name = value; OnPropertyChanged(); }
        }
        public bool IsSerial 
        { 
            get => _isSerial;
            set { _isSerial = value; OnPropertyChanged(); }
        }

        public virtual ObservableCollection<Film> Films { get; }
    }
}
