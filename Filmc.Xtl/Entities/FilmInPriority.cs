using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Entities
{
    public class FilmInPriority : Record
    {
        private int _id;
        private DateTime _creationTime;

        private Film _film = null!;

        public FilmInPriority()
        {
            _id = 0;
            _creationTime = DateTime.MinValue;
        }

        public int Id 
        { 
            get => _id; 
            set { _id = value; OnPropertyChanged(); }
        }
        public DateTime CreationTime 
        { 
            get => _creationTime; 
            set { _creationTime = value; OnPropertyChanged(); }
        }

        public Film Film
        {
            get => _film;
            private set { _film = value; OnPropertyChanged(); }
        }

        public override object Clone()
        {
            FilmInPriority clone = new FilmInPriority();

            clone._id = _id;
            clone._creationTime = _creationTime;

            return clone;
        }
    }
}
