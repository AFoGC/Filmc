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
using System.Xml.Linq;

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
        private const string backgorundImageNodeName = "BackgroundImage";
        private const string backgorundOpacityNodeName = "BackgroundOpacity";

        private readonly XmlDocument _settingsXml;

        private readonly ProfilesService _profilesService;

        private readonly AutoSaveService _autoSaveService;
        private readonly LanguageService _languageService;
        private readonly ScaleService _scaleService;
        private readonly BackgroundImageService _backgroundImageService;
        
        public SettingsService(ProfilesService profilesService, 
                               AutoSaveService autoSaveService, 
                               LanguageService languageService,
                               ScaleService scaleService,
                               BackgroundImageService backgroundImageService)
        {
            _settingsXml = new XmlDocument();

            _profilesService = profilesService;

            _autoSaveService = autoSaveService;
            _languageService = languageService;
            _scaleService = scaleService;
            _backgroundImageService = backgroundImageService;

            _profilesService.SelectedProfileChanged += OnProfileChanged;

            _autoSaveService.AutosaveIsEnableChanged += OnAutosaveEnableChanged;
            _autoSaveService.AutosaveIntervalChanged += OnAutosaveIntervalChanged;

            _languageService.LanguageChanged += OnLanguageChanged;
            _scaleService.ScaleChanged += OnScaleChanged;

            _backgroundImageService.ImageChanged += OnImageChanged;
            _backgroundImageService.OpacityChanged += OnOpacityChanged;
        }

        public ProfilesService ProfilesService => _profilesService;
        public AutoSaveService AutoSaveService => _autoSaveService;
        public LanguageService LanguageService => _languageService;
        public ScaleService ScaleService => _scaleService;
        public BackgroundImageService BackgroundImageService => _backgroundImageService;

        public void LoadSettings()
        {
            LoadDocument();

            LoadXmlProfile();

            LoadXmlLang();
            LoadXmlScale();
            LoadXmlAutosaveEnabled();
            LoadXmlAutosaveSeconds();
            LoadXmlImageName();
            LoadXmlImageOpacity();
        }

        private void LoadXmlProfile()
        {
            XmlNode? node = GetXmlNode(profileNodeName);

            if (node != null)
            {
                Profile? profile = _profilesService.Profiles.FirstOrDefault(x => x.Name == node.InnerText);

                if (profile != null)
                    _profilesService.SelectedProfile = profile;
            }
        }

        private void LoadXmlLang()
        {
            XmlNode? node = GetXmlNode(langNodeName);

            if (node != null)
                _languageService.SetLanguage(node.InnerText);
        }

        private void LoadXmlScale()
        {
            XmlNode? node = GetXmlNode(scaleNodeName);

            if (node != null)
            {
                if (Enum.IsDefined(typeof(ScaleEnum), node.InnerText))
                {
                    ScaleEnum scale = Enum.Parse<ScaleEnum>(node.InnerText);
                    _scaleService.SetScale(scale);
                }
            }
        }

        private void LoadXmlAutosaveEnabled()
        {
            XmlNode? node = GetXmlNode(autosaveNodeName);

            if (node != null)
                _autoSaveService.IsAutosaveEnable = Boolean.Parse(node.InnerText);
        }

        private void LoadXmlAutosaveSeconds()
        {
            XmlNode? node = GetXmlNode(autosaveSecondsNodeName);

            if (node != null)
                _autoSaveService.SaveTimerInterval = Double.Parse(node.InnerText);
        }

        private void LoadXmlImageName()
        {
            XmlNode? node = GetXmlNode(backgorundImageNodeName);

            if (node != null && node.InnerText != String.Empty)
                _backgroundImageService.ImageName = node.InnerText;
        }

        private void LoadXmlImageOpacity()
        {
            XmlNode? node = GetXmlNode(backgorundOpacityNodeName);

            if (node != null)
                _backgroundImageService.Opacity = Double.Parse(node.InnerText);
        }

        private void OnScaleChanged(ScaleEnum scale)
        {
            SetXmlNodeValue(scaleNodeName, scale.ToString());
        }

        private void OnLanguageChanged(CultureInfo culture)
        {
            SetXmlNodeValue(langNodeName, culture.Name);
        }

        private void OnProfileChanged(Profile profile)
        {
            SetXmlNodeValue(profileNodeName, profile.Name);
        }

        private void OnAutosaveEnableChanged()
        {
            SetXmlNodeValue(autosaveNodeName, _autoSaveService.IsAutosaveEnable.ToString());
        }

        private void OnAutosaveIntervalChanged()
        {
            SetXmlNodeValue(autosaveSecondsNodeName, _autoSaveService.SaveTimerInterval.ToString());
        }

        private void OnOpacityChanged(double obj)
        {
            SetXmlNodeValue(backgorundOpacityNodeName, _backgroundImageService.Opacity.ToString());
        }

        private void OnImageChanged(System.Windows.Media.Imaging.BitmapImage? obj)
        {
            SetXmlNodeValue(backgorundImageNodeName, _backgroundImageService.ImageName);
        }

        public void SaveSettings()
        {
            _settingsXml.Save(PathHelper.SettingsPath);
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

        private void SetXmlNodeValue(string name, string value)
        {
            XmlNode? node = GetXmlNode(name);

            if (node == null)
            {
                XmlNode mainNode = GetMainNode();

                node = _settingsXml.CreateElement(name);
                mainNode.AppendChild(node);
            }

            node.InnerText = value;
        }

        private XmlNode? GetXmlNode(string name)
        {
            XmlNode mainNode = GetMainNode();
            XmlNode? node = mainNode.SelectSingleNode(name);

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

            return node!;
        }
    }
}
