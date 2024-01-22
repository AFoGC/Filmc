using System;
using System.Collections.Generic;

namespace Filmc.Entities.Entities
{
    public partial class BookSource
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
            set => _id = value; 
        }
        public int BookId 
        { 
            get => _bookId; 
            set => _bookId = value; 
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

        public virtual Book Book { get; set; } = null!;
    }
}
