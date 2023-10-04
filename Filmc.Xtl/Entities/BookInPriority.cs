using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Entities
{
    public class BookInPriority : Record
    {
        private int _id;
        private DateTime _creationTime;

        private Book _book = null!;

        public BookInPriority()
        {
            _id = 0;
            _creationTime = DateTime.MinValue;
        }

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }
        public DateTime CreationTime
        {
            get => _creationTime;
            set { _creationTime = value; OnPropertyChanged(); }
        }

        public Book Book
        {
            get => _book;
            private set { _book = value; OnPropertyChanged(); }
        }

        public override object Clone()
        {
            BookInPriority clone = new BookInPriority();

            clone._id = _id;
            clone._creationTime = _creationTime;

            return clone;
        }
    }
}
