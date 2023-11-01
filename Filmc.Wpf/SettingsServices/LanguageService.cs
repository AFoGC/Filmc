using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.SettingsServices
{
    public class LanguageService
    {
        public event Action<CultureInfo>? LanguageChanged;

        private readonly List<CultureInfo> _languages;

        public IEnumerable<CultureInfo> Languages => _languages;
        public CultureInfo CurrentLanguage { get; private set; }

        public LanguageService()
        {
            CurrentLanguage = CultureInfo.GetCultureInfo("en");

            _languages = new List<CultureInfo>
            {
                CurrentLanguage,
                CultureInfo.GetCultureInfo("ru"),
                CultureInfo.GetCultureInfo("uk")
            };
        }

        public bool SetLanguage(CultureInfo lang)
        {
            bool hasLanguage = _languages.Contains(lang);

            if (hasLanguage)
            {
                CurrentLanguage = lang;
                LanguageChanged?.Invoke(lang);
            }

            return hasLanguage;
        }

        public bool SetLanguage(string name)
        {
            var culture = CultureInfo.GetCultureInfo(name);
            return SetLanguage(culture);
        }
    }
}
