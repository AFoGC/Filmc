using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Xtl.EntityProperties
{
    public class Source : ICloneable, INotifyPropertyChanged
    {
        private string _name;
        private string _url;

        public Source()
        {
            _name = String.Empty;
            _url = String.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Url
        {
            get => _url;
            set { _url = value; OnPropertyChanged(); }
        }

        public object Clone()
        {
            Source source = new Source();

            source._name = _name;
            source._url = _url;

            return source;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
            this.PropertyChanged?.Invoke(this, e);
        }
    }
}
