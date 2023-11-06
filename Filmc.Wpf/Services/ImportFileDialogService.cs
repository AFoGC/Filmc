using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Services
{
    public class ImportFileDialogService
    {
        public string? FileName { get; private set; }

        public bool OpenFileDialog()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Films";
            dialog.DefaultExt = ".xml";
            dialog.Filter = "Xml documents (.xml)|*.xml";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                FileName = dialog.FileName;
                return true;
            }
            else
            {
                FileName = null;
                return false;
            }
        }
    }
}
