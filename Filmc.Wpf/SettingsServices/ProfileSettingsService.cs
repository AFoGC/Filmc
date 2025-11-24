using Filmc.Entities.Repositories;
using Filmc.Wpf.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Filmc.Wpf.SettingsServices
{
    public class ProfileSettingsService : SettingsService
    {
        private const string filmsMarkSystemNodeName = "FilmsMarkSystem";
        private const string booksMarkSystemNodeName = "BooksMarkSystem";

        private readonly string _settingsFilePath;
        private readonly RepositoriesFacade _repositories;

        private int _filmsMarkSystem;
        private int _booksMarkSystem;

        public ProfileSettingsService(RepositoriesFacade repositories, string profileName)
        {
            _repositories = repositories;
            _settingsFilePath = PathHelper.GetProfileSettingsPath(profileName);

            _filmsMarkSystem = 6;
            _booksMarkSystem = 6;
        }

        public int FilmsMarkSystem
        {
            get => _filmsMarkSystem;
            set
            { 
                _filmsMarkSystem = value;

                _repositories.Films.MarkSystem = value;
                _repositories.FilmCategories.MarkSystem = value;

                SetXmlNodeValue(filmsMarkSystemNodeName, value.ToString());
            }
        }

        public int BooksMarkSystem
        {
            get => _booksMarkSystem;
            set
            {
                _booksMarkSystem = value;

                _repositories.Books.MarkSystem = value;
                _repositories.BookCategories.MarkSystem = value;

                SetXmlNodeValue(booksMarkSystemNodeName, value.ToString());
            }
        }

        private void LoadXmlFilmsMarkSystem()
        {
            XmlNode? node = GetXmlNode(filmsMarkSystemNodeName);

            if (node != null)
                FilmsMarkSystem = Int32.Parse(node.InnerText);
        }

        private void LoadXmlBooksMarkSystem()
        {
            XmlNode? node = GetXmlNode(booksMarkSystemNodeName);

            if (node != null)
                BooksMarkSystem = Int32.Parse(node.InnerText);
        }

        public void LoadSettings()
        {
            base.LoadDocument(_settingsFilePath);

            LoadXmlFilmsMarkSystem();
            LoadXmlBooksMarkSystem();
        }

        public void SaveSettings()
        {
            base.SaveSettings(_settingsFilePath);
        }
    }
}
