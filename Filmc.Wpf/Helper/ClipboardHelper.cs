using Filmc.Xtl.EntityProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Filmc.Wpf.Helper
{
    public static class ClipboardHelper
    {
        public static void CopyToClipboard(string text)
        {
            Clipboard.SetText(text);
        }

        public static void CopySourceUrlToClipboard(IEnumerable<Source> sources)
        {
            Source? source = sources.FirstOrDefault();

            if (source != null)
                Clipboard.SetText(source.Url);
        }
    }
}
