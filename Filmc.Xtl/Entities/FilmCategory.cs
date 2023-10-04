﻿using Filmc.Xtl.EntityProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Entities
{
    public class FilmCategory : Record
    {
        private int _id;
        private string _name;
        private string _hideName;
        private int _priority;

        private Mark _mark;

        private readonly RecordsCollection<Film> _films;

        public FilmCategory()
        {
            _id = 0;
            _name = String.Empty;
            _hideName = String.Empty;
            _priority = 0;

            _mark = new Mark();

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
        public string HideName 
        { 
            get => _hideName; 
            set { _hideName = value; OnPropertyChanged(); }
        }
        public int Priority 
        { 
            get => _priority; 
            set { _priority = value; OnPropertyChanged(); }
        }
        public Mark Mark 
        { 
            get => _mark;
            private set => _mark = value;
        }

        public RecordsCollection<Film> Films => _films;

        public override object Clone()
        {
            FilmCategory category = new FilmCategory();

            category._id = _id;
            category._name = _name;
            category._hideName = _hideName;
            category._priority = _priority;

            category._mark.RawMark = _mark.RawMark;

            return category;
        }
    }
}
