using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Entities.PropertyTypes
{
    public class Mark : INotifyPropertyChanged
    {
        private int _maxMark;
        private int? _rawMark;

        public const int MaxRawMark = 300;

        public Mark()
        {
            _maxMark = 6;
            _rawMark = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int? RawMark
        {
            get => _rawMark;
            set { _rawMark = value; OnPropertyChanged(); OnPropertyChanged(nameof(FormatedMark)); }
        }

        public int MarkSystem
        {
            get => _maxMark;
            set { _maxMark = value; OnPropertyChanged(); OnPropertyChanged(nameof(FormatedMark)); }
        }

        public int? FormatedMark
        {
            get
            {
                if (_rawMark != null)
                {
                    int modifier = MaxRawMark / MarkSystem;
                    int outMark = 0;
                    for (int i = 0; i <= MarkSystem; i++)
                    {
                        outMark = modifier * i;
                        if (outMark >= RawMark)
                        {
                            return i;
                        }
                    }

                    return null;
                }
                else
                {
                    return null;
                }

            }
            set
            {
                if (value != null)
                {
                    int modifier = MaxRawMark / MarkSystem;
                    RawMark = modifier * value;
                }
                else
                {
                    RawMark = null;
                }

                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
        }
    }
}
