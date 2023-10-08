using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Entities
{
    public class FilmHasTag : Record
    {
        private int _id;
        private int _filmId;
        private int _tagId;

        private Film _film = null!;
        private FilmTag _tag = null!;

        public FilmHasTag()
        {
            _id = 0;
            _filmId = 0;
            _tagId = 0;
        }

        public int Id 
        { 
            get => _id; 
            set { _id = value; OnPropertyChanged(); }
        }
        public int FilmId 
        { 
            get => _filmId; 
            set { _filmId = value; OnPropertyChanged(); }
        }
        public int TagId 
        { 
            get => _tagId; 
            set { _tagId = value; OnPropertyChanged(); }
        }

        public Film Film
        { 
            get => _film;
            private set { _film = value; OnPropertyChanged(); } 
        }
        public FilmTag Tag
        { 
            get => _tag; 
            private set { _tag = value; OnPropertyChanged(); }
            
        }

        public override object Clone()
        {
            FilmHasTag clone = new FilmHasTag();

            clone._id = _id;
            clone._filmId = _filmId;
            clone._tagId = _tagId;

            return clone;
        }
    }
}
