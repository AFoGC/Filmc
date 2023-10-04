using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Entities
{
    public class FilmTag : Record
    {
        private int _id;
        private string _name;

        private readonly RecordsCollection<FilmHasTag> _hasFilms;

        public FilmTag()
        {
            _id = 0;
            _name = String.Empty;

            _hasFilms = new RecordsCollection<FilmHasTag>();
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

        public RecordsCollection<FilmHasTag> HasFilms => _hasFilms;

        public override object Clone()
        {
            FilmTag tag = new FilmTag();

            tag._id = _id;
            tag._name = _name;

            return tag;
        }
    }
}
