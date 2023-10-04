using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Entities
{
    public class BookTag : Record
    {
        private int _id;
        private string _name;

        private readonly RecordsCollection<BookHasTag> _hasBooks;

        public BookTag()
        {
            _id = 0;
            _name = String.Empty;

            _hasBooks = new RecordsCollection<BookHasTag>();
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

        public RecordsCollection<BookHasTag> HasBooks => _hasBooks;

        public override object Clone()
        {
            BookTag tag = new BookTag();

            tag._id = _id;
            tag._name = _name;

            return tag;
        }
    }
}
