using System;
using System.Collections.Generic;

namespace Filmc.Entities.Entities
{
    public partial class FilmsInPriority : BaseEntity
    {
        private int _id;
        private DateTime _creationDate;

        public int Id 
        { 
            get => _id; 
            set { _id = value; OnPropertyChanged(); }
        }
        public DateTime CreationDate 
        { 
            get => _creationDate; 
            set { _creationDate = value; OnPropertyChanged(); }
        }

        public virtual Film Film { get; set; } = null!;
    }
}
