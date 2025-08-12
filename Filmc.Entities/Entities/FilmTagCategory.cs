using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Entities.Entities
{
    public class FilmTagCategory : BaseEntity, ITagCategory
    {
        private int _id;
        private string _name = null!;

        private byte _colorA;
        private byte _colorR;
        private byte _colorG;
        private byte _colorB;

        public FilmTagCategory()
        {
            _name = String.Empty;
            _colorA = 255;
            _colorR = 130;
            _colorG = 130;
            _colorB = 130;

            Tags = new ObservableCollection<FilmTag>();
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
        public byte ColorA
        {
            get => _colorA;
            set { _colorA = value; OnPropertyChanged(); }
        }
        public byte ColorR
        {
            get => _colorR;
            set { _colorR = value; OnPropertyChanged(); }
        }
        public byte ColorG
        {
            get => _colorG;
            set { _colorG = value; OnPropertyChanged(); }
        }
        public byte ColorB
        {
            get => _colorB;
            set { _colorB = value; OnPropertyChanged(); }
        }

        public virtual ObservableCollection<FilmTag> Tags { get; }
    }
}
