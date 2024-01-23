using System;
using System.Collections.Generic;

namespace Filmc.Entities.Entities
{
    public partial class BookSource : BaseEntity
    {
        private int _id;
        private int _bookId;
        private string _name = null!;
        private string _url = null!;

        public BookSource()
        {
            Name = String.Empty;
            Url = String.Empty;
        }

        public int Id 
        { 
            get => _id; 
            set { _id = value; OnPropertyChanged(); }
        }
        public int BookId 
        { 
            get => _bookId; 
            set { _bookId = value; OnPropertyChanged(); }
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

        public virtual Book Book { get; set; } = null!;
    }
}
