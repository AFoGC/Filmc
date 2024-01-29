using System;
using System.Collections.Generic;

namespace Filmc.Entities.Entities
{
    public partial class BooksInPriority : BaseEntity
    {
        private int _id;
        private DateTime _creationDate;

        private Book book = null!;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }
        public DateTime CreationTime
        {
            get => _creationDate;
            set { _creationDate = value; OnPropertyChanged(); }
        }

        public virtual Book Book 
        { 
            get => book;
            set { book = value; OnPropertyChanged(); } 
        }
    }
}
