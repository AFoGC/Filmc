﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Filmc.Entities.PropertyTypes;

namespace Filmc.Entities.Entities
{
    public partial class FilmCategory : BaseEntity
    {
        private int _id;
        private string _name = null!;
        private string _hideName = null!;

        public FilmCategory()
        {
            Name = String.Empty;
            HideName = String.Empty;
            Mark = new Mark();

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
        public string HideName 
        { 
            get => _hideName; 
            set { _hideName = value; OnPropertyChanged(); }
        }
        internal int? RawMark
        {
            get => Mark.RawMark;
            set => Mark.RawMark = value;
        }

        public virtual Mark Mark { get; }

        public virtual ICollection<Film> Films { get; set; }
    }
}