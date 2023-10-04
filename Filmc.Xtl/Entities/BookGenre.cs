using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Entities
{
    public class BookGenre : Record
    {
        private int _id;
        private string _name;

        private readonly RecordsCollection<Book> _books;

        public BookGenre()
        {
            _id = 0;
            _name = String.Empty;

            _books = new RecordsCollection<Book>();
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

        public RecordsCollection<Book> Books => _books;

        public override object Clone()
        {
            BookGenre genre = new BookGenre();

            genre._id = _id;
            genre._name = _name;

            return genre;
        }
    }
}
