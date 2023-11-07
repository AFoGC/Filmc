using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Helper
{
    public static class StringExtension
    {
        public static bool SearchBy(this string main, string search)
        {
            return main.ToLowerInvariant().Contains(search);
        }
    }
}
