using System;
using System.Collections.Generic;

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

            Films = new HashSet<Film>();
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

        public virtual ICollection<Film> Films { get; set; }
    }
}
