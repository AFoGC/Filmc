using Filmc.Entities.Entities;
using Filmc.Wpf.Helper;
using Filmc.Wpf.Repositories;
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
        private readonly RepositoriesFacade _tablesContext;

        private bool _isLoaded;
        private bool _isChangesSaved;

        private string _name;

        public string Name
        {
            get { return _name; }
        }

        public bool IsLoaded => _isLoaded;
        public bool IsChangesSaved => _isChangesSaved;

        public event Action? InfoChanged;

        public RepositoriesFacade TablesContext
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

            string profileDirectory = PathHelper.GetProfileDirectoryPath(_name);
            _tablesContext = new RepositoriesFacade(profileDirectory);

            _tablesContext.TablesSaved += OnTablesContextSaved;
        }

        public void SaveTables()
        {
            LoadTables();

            //string profileFile = PathHelper.GetProfileFilePath(_name);
            _tablesContext.SaveChanges();
        }

        private void LoadTables()
        {
            if (_isLoaded == false)
            {
                string profileDirectory = PathHelper.GetProfileDirectoryPath(_name);
                string profileFile = PathHelper.GetProfileFilePath(_name);

                Directory.CreateDirectory(profileDirectory);
                _tablesContext.Migrate();

                if (File.Exists(profileFile) == false)
                {
                    _tablesContext.FilmGenres.Add(new FilmGenre { Id = 1, Name = "Movie", IsSerial = false });
                    _tablesContext.FilmGenres.Add(new FilmGenre { Id = 2, Name = "Series", IsSerial = true });

                    _tablesContext.BookGenres.Add(new BookGenre { Id = 1, Name = "Book" });

                    Directory.CreateDirectory(profileDirectory);
                    _tablesContext.SaveChanges();
                }

                _isLoaded = true;
                _isChangesSaved = true;
                ConfigureInfoChangedEvent();
            }
        }

        private void ConfigureInfoChangedEvent()
        {
            /*
            foreach (var table in _tablesContext.Tables)
            {
                table.CollectionChanged += OnTableCollectionChanged;
                table.RecordsPropertyChanged += OnTableRecordsPropertyChanged;
            }
            */
        }

        private void OnTableRecordsPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnInfoChanged();
        }

        private void OnTableCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnInfoChanged();
        }

        private void OnInfoChanged()
        {
            _isChangesSaved = false;
            InfoChanged?.Invoke();
        }

        private void OnTablesContextSaved(RepositoriesFacade sender)
        {
            _isChangesSaved = true;
        }
    }
}
