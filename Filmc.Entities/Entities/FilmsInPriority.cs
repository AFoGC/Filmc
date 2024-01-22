using System;
using System.Collections.Generic;

namespace Filmc.Entities.Entities
{
    public partial class FilmsInPriority
    {
        private int _id;
        private DateTime _creationDate;

        public int Id 
        { 
            get => _id; 
            set => _id = value; 
        }
        public DateTime CreationDate 
        { 
            get => _creationDate; 
            set => _creationDate = value; 
        }

        public virtual Film IdNavigation { get; set; } = null!;
    }
}
