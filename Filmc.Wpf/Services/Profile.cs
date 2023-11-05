using Filmc.Wpf.Helper;
using Filmc.Xtl;
using Filmc.Xtl.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Services
{
    public class Profile
    {
        private readonly TablesContext _tablesContext;

        private bool _isLoaded;
        private string _name;

        public string Name
        {
            get { return _name; }
        }

        public event Action? InfoChanged;

        public TablesContext TablesContext
        {
            get
            {
                LoadTables();

                return _tablesContext;
            }
        }

        public Profile(string name)
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
                ConfigureInfoChangedEvent();
            }
        }

        private void ConfigureInfoChangedEvent()
        {
            foreach (var table in _tablesContext.Tables)
            {
                table.CollectionChanged += OnTableCollectionChanged;
                table.RecordsPropertyChanged += OnTableRecordsPropertyChanged;
            }
        }

        private void OnTableRecordsPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            InfoChanged?.Invoke();
        }

        private void OnTableCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            InfoChanged?.Invoke();
        }
    }
}
