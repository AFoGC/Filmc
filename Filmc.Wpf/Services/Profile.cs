using Filmc.Entities.Context;
using Filmc.Entities.Entities;
using Filmc.Wpf.Helper;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.SettingsServices;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Filmc.Wpf.Services
{
    public class Profile
    {
        private ProfileSettingsService? _profileSettings;
        private RepositoriesFacade? _tablesContext;
        private bool _isChangesSaved;

        private string _name;

        public Profile(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public bool IsLoaded => _tablesContext != null;
        public bool IsChangesSaved => _isChangesSaved;

        public event Action? InfoChanged;
        public event Action? ProfileSaved;

        public RepositoriesFacade TablesContext
        {
            get
            {
                if (_tablesContext == null)
                    _tablesContext = LoadTables();

                return _tablesContext;
            }
        }

        public ProfileSettingsService Settings
        {
            get
            {
                if (_profileSettings == null)
                    _profileSettings = LoadSettings();

                return _profileSettings;
            }
        }

        public void DeleteTempFile(bool saveChanges)
        {
            string profileDirectoryPath = PathHelper.GetProfileDirectoryPath(_name);
            string tempFilePath = Path.Combine(profileDirectoryPath, "Temp.db");

            PathHelper.ClearSqlitePool(tempFilePath);
            TablesContext.DeleteDbFile();
        }

        public void SaveTables()
        {
            string profileDirectoryPath = PathHelper.GetProfileDirectoryPath(_name);
            string mainFilePath = Path.Combine(profileDirectoryPath, "Info.db");
            string tempFilePath = Path.Combine(profileDirectoryPath, "Temp.db");

            TablesContext.SaveChanges();
            PathHelper.ClearSqlitePool(tempFilePath);
            File.Copy(tempFilePath, mainFilePath, true);

            OnProfileSaved();

            Settings.SaveSettings();
        }

        private RepositoriesFacade LoadTables()
        {
            string profileDirectoryPath = PathHelper.GetProfileDirectoryPath(_name);
            string mainFilePath = Path.Combine(profileDirectoryPath, "Info.db");
            string tempFilePath = Path.Combine(profileDirectoryPath, "Temp.db");

            string connectionString = "Datasource=" + tempFilePath;
            SqliteConnection connection = new SqliteConnection(connectionString);

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            var opt = optionsBuilder.UseSqlite(connection).Options;
            FilmsContext filmsContext = new FilmsContext(opt);

            Directory.CreateDirectory(profileDirectoryPath);

            if (File.Exists(mainFilePath) == false)
            {
                CreateDbFile(mainFilePath);
            }

            if (File.Exists(tempFilePath) == false)
            {
                File.Copy(mainFilePath, tempFilePath);
            }

            filmsContext.Database.Migrate();
            RepositoriesFacade repositories = new RepositoriesFacade(filmsContext);

            _isChangesSaved = true;
            ConfigureInfoChangedEvent(repositories);

            return repositories;
        }

        private void CreateDbFile(string path)
        {
            SqliteConnection connection = PathHelper.GetSqliteConnection(path);

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            var opt = optionsBuilder.UseSqlite(connection).Options;
            FilmsContext filmsContext = new FilmsContext(opt);

            filmsContext.Database.Migrate();
            filmsContext.FilmGenres.Add(new FilmGenre { Id = 1, Name = "Movie", IsSerial = false });
            filmsContext.FilmGenres.Add(new FilmGenre { Id = 2, Name = "Series", IsSerial = true });

            filmsContext.BookGenres.Add(new BookGenre { Id = 1, Name = "Book" });
            filmsContext.SaveChanges();

            SqliteConnection.ClearPool(connection);

            filmsContext.Dispose();
        }

        private ProfileSettingsService LoadSettings()
        {
            RepositoriesFacade repositories = TablesContext;
            var settings = new ProfileSettingsService(repositories, _name);
            settings.LoadSettings();

            return settings;
        }

        private void ConfigureInfoChangedEvent(RepositoriesFacade repositories)
        {
            foreach (var repository in repositories.Repositories)
            {
                repository.ItemInCollectionChanged += OnTableRecordsPropertyChanged;
                repository.CollectionChanged += OnTableCollectionChanged;
            }
        }

        private void OnTableRecordsPropertyChanged()
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

        private void OnProfileSaved()
        {
            _isChangesSaved = true;
            ProfileSaved?.Invoke();
        }
    }
}
