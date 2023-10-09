using Filmc.Wpf.Helper;
using Filmc.Xtl;
using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Models
{
    public class ProfileModel
    {
        private readonly TablesContext _tablesContext;

        private bool _isLoaded;
        private string _name;

        public string Name
        {
            get { return _name; }
        }

        public TablesContext TablesContext
        {
            get
            {
                LoadTables();

                return _tablesContext;
            }
        }

        public ProfileModel(string name)
        {
            _name = name;
            _tablesContext = new TablesContext();
        }

        public void SaveTables()
        {
            LoadTables();

            string profileFile = PathHelper.GetProfileFilePath(_name);
            _tablesContext.Save(profileFile);
        }

        private void LoadTables()
        {
            if (_isLoaded == false)
            {
                string profileDirectory = PathHelper.GetProfileDirectoryPath(_name);
                string profileFile = PathHelper.GetProfileFilePath(_name);

                if (File.Exists(profileFile))
                {
                    _tablesContext.Load(profileFile);
                }
                else
                {
                    _tablesContext.FilmGenres.Add(new FilmGenre { Name = "Movie", IsSerial = false });
                    _tablesContext.FilmGenres.Add(new FilmGenre { Name = "Serial", IsSerial = true });

                    _tablesContext.BookGenres.Add(new BookGenre { Name = "Book" });

                    Directory.CreateDirectory(profileDirectory);
                    _tablesContext.Save(profileFile);
                }

                _isLoaded = true;
            }
        }
    }
}
