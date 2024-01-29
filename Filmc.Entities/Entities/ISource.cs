using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Entities.Entities
{
    public interface ISource
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
