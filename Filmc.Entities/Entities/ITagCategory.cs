using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Filmc.Entities.Entities
{
    public interface ITagCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte ColorA { get; set; }
        public byte ColorR { get; set; }
        public byte ColorG { get; set; }
        public byte ColorB { get; set; }
    }
}
