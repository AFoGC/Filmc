using System;
using System.Collections.Generic;
using Filmc.Entities.PropertyTypes;

namespace Filmc.Entities.Entities
{
    public partial class BookCategory
    {
        private int _id;
        private string _name = null!;
        private string _hideName = null!;

        public BookCategory()
        {
            Name = String.Empty;
            HideName = String.Empty;
            Mark = new Mark();

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
        public string HideName 
        { 
            get => _hideName; 
            set => _hideName = value; 
        }
        public int? RawMark
        {
            get => Mark.RawMark;
            set => Mark.RawMark = value;
        }

        public virtual Mark Mark { get; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
