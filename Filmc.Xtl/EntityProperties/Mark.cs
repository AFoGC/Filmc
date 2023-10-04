using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Xtl.EntityProperties
{
    public class Mark : INotifyPropertyChanged
    {
        private int _maxMark;
        private int _rawMark;

        public const int MaxRawMark = 300;

        public Mark()
        {
            _maxMark = 6;
            _rawMark = 0;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int RawMark
        {
            get => _rawMark;
            set { _rawMark = value; OnPropertyChanged(); }
        }

        public int MarkSystem => _maxMark;

        public void SetMarkSystem(int maxMark)
        {
            _maxMark = maxMark;
            OnPropertyChanged(nameof(MarkSystem));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
        }
    }
}
