﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Filmc.Entities.Entities
{
    public partial class FilmTag : BaseEntity
    {
        private int _id;
        private string _name = null!;

        public FilmTag()
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

        public virtual ObservableCollection<Film> Films { get; }
    }
}
