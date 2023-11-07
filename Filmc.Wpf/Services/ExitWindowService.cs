using Filmc.Wpf.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Filmc.Wpf.Services
{
    public class ExitWindowService
    {
        public bool? ShowDialog()
        {
            ExitWindow window = new ExitWindow();

            bool? result = window.ShowDialog();

            Save = window.Save;
            Close = window.CloseProg;

            return result;
        }
        public bool Save { get; private set; }
        public bool Close { get; private set; }
    }
}
