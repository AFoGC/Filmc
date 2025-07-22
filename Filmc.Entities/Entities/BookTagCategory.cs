using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Entities.Entities
{
    public class BookTagCategory : BaseEntity
    {
        private int _id;
        private string _name = null!;
        private string _color = null!;

        public BookTagCategory()
        {
            Name = String.Empty;
            Color = "#828282";

            Tags = new ObservableCollection<BookTag>();
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
        public string Color
        {
            get => _color;
            set { _color = value; OnPropertyChanged(); }
        }

        public virtual ObservableCollection<BookTag> Tags { get; }
    }
}
