using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Updater.Module
{
    public class UpdateInfo
    {
        public string Tag { get; set; }
        public string Description { get; set; }

        public UpdateInfo()
        {
            Tag = String.Empty;
            Description = String.Empty;
        }
    }
}
