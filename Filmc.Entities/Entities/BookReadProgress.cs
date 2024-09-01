using Filmc.Entities.PropertyTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Entities.Entities
{
    public class BookReadProgress : BaseEntity
    {
        private int _id;
        private string _name = null!;

        public BookReadProgress()
        {
            _id = 0;
            _name = String.Empty;

            Books = new NotifyCollection<Book>();
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

        public virtual NotifyCollection<Book> Books { get; }
    }
}
