using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Entities
{
    public class FilmGenre : Record
    {
        private int _id;
        private string _name;
        private bool _isSerial;

        private readonly RecordsCollection<Film> _films;

        public FilmGenre()
        {
            _id = 0;
            _name = String.Empty;
            _isSerial = false;

            _films = new RecordsCollection<Film>();
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

        public RecordsCollection<Film> Films => _films;

        public override object Clone()
        {
            FilmGenre genre = new FilmGenre();

            genre._id = _id;
            genre._name = _name;
            genre._isSerial = _isSerial;

            return genre;
        }
    }
}
