﻿using System;
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

        public int MarkSystem
        {
            get => _maxMark;
            set { _maxMark = value; OnPropertyChanged(); }
        }

        public int FormatedMark
        {
            get
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

                return 0;
            }
            set
            {
                int modifier = MaxRawMark / MarkSystem;
                RawMark = modifier * value;
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
