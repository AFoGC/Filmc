using Filmc.Entities.Entities;
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

        public static void CopySourceUrlToClipboard(IEnumerable<BookSource> sources)
        {
            BookSource? source = sources.FirstOrDefault();

            if (source != null)
                Clipboard.SetText(source.Url);
        }

        public static void CopySourceUrlToClipboard(IEnumerable<FilmSource> sources)
        {
            FilmSource? source = sources.FirstOrDefault();

            if (source != null)
                Clipboard.SetText(source.Url);
        }
    }
}
