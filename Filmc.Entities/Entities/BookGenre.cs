using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Filmc.Entities.Entities
{
    public partial class BookGenre
    {
        private int _id;
        private string _name = null!;

        public BookGenre()
        {
            Name = String.Empty;
            Books = new HashSet<Book>();
        }

        public int Id 
        { 
            get => _id; 
            set => _id = value; 
        }
        public string Name 
        { 
            get => _name; 
            set => _name = value; 
        }
        public virtual ICollection<Book> Books { get; set; }
    }
}
