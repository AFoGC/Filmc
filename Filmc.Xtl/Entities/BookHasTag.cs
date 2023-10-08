using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xtl;

namespace Filmc.Xtl.Entities
{
    public class BookHasTag : Record
    {
        private int _id;
        private int _filmId;
        private int _tagId;

        private Book _book = null!;
        private BookTag _tag = null!;

        public BookHasTag()
        {
            _id = 0;
            _filmId = 0;
            _tagId = 0;
        }

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }
        public int BookId
        {
            get => _filmId;
            set { _filmId = value; OnPropertyChanged(); }
        }
        public int TagId
        {
            get => _tagId;
            set { _tagId = value; OnPropertyChanged(); }
        }

        public Book Book
        {
            get => _book;
            private set { _book = value; OnPropertyChanged(); }
        }
        public BookTag Tag
        {
            get => _tag;
            private set { _tag = value; OnPropertyChanged(); }

        }

        public override object Clone()
        {
            BookHasTag clone = new BookHasTag();

            clone._id = _id;
            clone._filmId = _filmId;
            clone._tagId = _tagId;

            return clone;
        }
    }
}
