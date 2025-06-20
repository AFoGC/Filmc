﻿using System;
using System.Collections.Generic;

namespace Filmc.Entities.Entities
{
    public partial class FilmSource : BaseEntity, ISource
    {
        private int _id;
        private int _filmId;
        private string _name = null!;
        private string _url = null!;
        private int _indexInList;

        private Film film = null!;

        public FilmSource()
        {
            Name = String.Empty;
            Url = String.Empty;
            _indexInList = 0;
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
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
        public string Url
        {
            get => _url;
            set { _url = value; OnPropertyChanged(); }
        }
        public int IndexInList
        {
            get => _indexInList;
            set { _indexInList = value; OnPropertyChanged(); }
        }

        public virtual Film Film 
        { 
            get => film;
            set { film = value; OnPropertyChanged(); }
        }
    }
}
