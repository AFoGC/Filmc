using System;
using System.Collections.Generic;

namespace Filmc.Entities.Entities
{
    public partial class FilmSource
    {
        private int _id;
        private int _filmId;
        private string _name = null!;
        private string _url = null!;

        public FilmSource()
        {
            Name = String.Empty;
            Url = String.Empty;
        }

        public int Id 
        { 
            get => _id; 
            set => _id = value; 
        }
        public int FilmId 
        { 
            get => _filmId; 
            set => _filmId = value; 
        }
        public string Name 
        { 
            get => _name; 
            set => _name = value; 
        }
        public string Url 
        { 
            get => _url; 
            set => _url = value; 
        }

        public virtual Film Film { get; set; } = null!;
    }
}
