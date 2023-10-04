using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Xtl.EntityProperties
{
    public class Source : ICloneable
    {
        private string _name;
        private string _url;

        public Source()
        {
            _name = String.Empty;
            _url = String.Empty;
        }

        public object Clone()
        {
            Source source = new Source();

            source._name = _name;
            source._url = _url;

            return source;
        }
    }
}
