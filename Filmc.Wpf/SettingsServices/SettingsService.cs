using Filmc.Wpf.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Filmc.Wpf.SettingsServices
{
    public abstract class SettingsService
    {
        private const string mainNodeName = "SettingsFields";

        private readonly XmlDocument _settingsXml;

        public SettingsService()
        {
            _settingsXml = new XmlDocument();
        }

        protected void SaveSettings(string path)
        {
            _settingsXml.Save(path);
        }

        protected bool LoadDocument(string path)
        {
            if (File.Exists(path))
            {
                _settingsXml.Load(path);
                return true;
            }

            return false;
        }

        protected void SetXmlNodeValue(string name, string value)
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

        protected XmlNode? GetXmlNode(string name)
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
