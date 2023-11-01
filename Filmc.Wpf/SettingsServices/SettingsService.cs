using Filmc.Wpf.Helper;
using Filmc.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Filmc.Wpf.SettingsServices
{
    public class SettingsService
    {
        private const string mainNodeName = "SettingsFields";
        private const string profileNodeName = "UsedProfile";
        private const string langNodeName = "Lang";
        private const string scaleNodeName = "Scale";
        private const string autosaveNodeName = "IsSaveTimerEnabled";
        private const string autosaveSecondsNodeName = "SaveTimerSeconds";

        private readonly XmlDocument _settingsXml;

        private readonly ProfilesService _profilesService;

        private readonly AutoSaveService _autoSaveService;
        private readonly LanguageService _languageService;
        private readonly ScaleService _scaleService;
        
        public SettingsService(ProfilesService profilesService, 
                               AutoSaveService autoSaveService, 
                               LanguageService languageService,
                               ScaleService scaleService)
        {
            _settingsXml = new XmlDocument();

            _profilesService = profilesService;

            _autoSaveService = autoSaveService;
            _languageService = languageService;
            _scaleService = scaleService;

            _profilesService.SelectedProfileChanged += OnProfileChanged;

            _autoSaveService.AutosaveIsEnableChanged += OnAutosaveEnableChanged;
            _autoSaveService.AutosaveIntervalChanged += OnAutosaveIntervalChanged;

            _languageService.LanguageChanged += OnLanguageChanged;
            _scaleService.ScaleChanged += OnScaleChanged;
        }

        public ProfilesService ProfilesService => _profilesService;
        public AutoSaveService AutoSaveService => _autoSaveService;
        public LanguageService LanguageService => _languageService;
        public ScaleService ScaleService => _scaleService;

        public void LoadSettings()
        {
            if (LoadDocument() == false)
            {
                OnProfileChanged(_profilesService.SelectedProfile);

                OnScaleChanged(_scaleService.CurrentScale);
                OnLanguageChanged(_languageService.CurrentLanguage);
                OnAutosaveEnableChanged();
                OnAutosaveIntervalChanged();
            }

            LoadXmlProfile();

            LoadXmlLang();
            LoadXmlScale();
            LoadXmlAutosaveEnabled();
            LoadXmlAutosaveSeconds();
        }

        private void OnScaleChanged(ScaleEnum scale)
        {
            XmlNode node = GetXmlNode(scaleNodeName);

            int scaleCode = (int)scale;
            node.InnerText = scaleCode.ToString();
        }

        private void OnLanguageChanged(CultureInfo culture)
        {
            XmlNode node = GetXmlNode(langNodeName);
            node.InnerText = culture.Name;
        }

        private void OnProfileChanged(Profile profile)
        {
            XmlNode node = GetXmlNode(profileNodeName);
            node.InnerText = profile.Name;
        }

        private void OnAutosaveEnableChanged()
        {
            XmlNode node = GetXmlNode(autosaveNodeName);
            node.InnerText = _autoSaveService.IsAutosaveEnable.ToString();
        }

        private void OnAutosaveIntervalChanged()
        {
            XmlNode node = GetXmlNode(autosaveSecondsNodeName);
            node.InnerText = _autoSaveService.SaveTimerInterval.ToString();
        }

        private void LoadXmlProfile()
        {
            XmlNode node = GetXmlNode(profileNodeName);
            Profile? profile = _profilesService.Profiles.FirstOrDefault(x => x.Name == node.InnerText);

            if (profile != null)
                _profilesService.SelectedProfile = profile;
        }

        private void LoadXmlLang()
        {
            XmlNode node = GetXmlNode(langNodeName);
            _languageService.SetLanguage(node.InnerText);
        }

        private void LoadXmlScale()
        {
            XmlNode node = GetXmlNode(scaleNodeName);
            int scaleCode = Int32.Parse(node.InnerText);
            _scaleService.SetScale(scaleCode);
        }

        private void LoadXmlAutosaveEnabled()
        {
            XmlNode node = GetXmlNode(autosaveNodeName);
            _autoSaveService.IsAutosaveEnable = Boolean.Parse(node.InnerText);
        }

        private void LoadXmlAutosaveSeconds()
        {
            XmlNode node = GetXmlNode(autosaveSecondsNodeName);
            _autoSaveService.SaveTimerInterval = Double.Parse(node.InnerText);
        }

        private bool LoadDocument()
        {
            if (File.Exists(PathHelper.SettingsPath))
            {
                _settingsXml.Load(PathHelper.SettingsPath);
                return true;
            }

            return false;
        }

        private XmlNode GetXmlNode(string name)
        {
            XmlNode mainNode = GetMainNode();
            XmlNode? node = mainNode.SelectSingleNode(name);

            if (node == null)
            {
                node = _settingsXml.CreateElement(name);
                node = mainNode.AppendChild(node);
            }

            return node;
        }

        private XmlNode GetMainNode()
        {
            XmlNode? node = _settingsXml.SelectSingleNode(mainNodeName);

            if (node == null || node.Name != mainNodeName)
            {
                node = _settingsXml.CreateElement(mainNodeName);
                node = _settingsXml.AppendChild(node);
            }

            return node;
        }
    }
}
