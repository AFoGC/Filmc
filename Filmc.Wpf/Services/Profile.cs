using Filmc.Entities.Context;
using Filmc.Entities.Entities;
using Filmc.Wpf.Helper;
using Filmc.Wpf.Repositories;
using Filmc.Wpf.SaveConverters;
using Microsoft.EntityFrameworkCore;
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
        private RepositoriesFacade? _tablesContext;
        private bool _isChangesSaved;

        private string _name;

        public string Name
        {
            get { return _name; }
        }

        public bool IsLoaded => _tablesContext != null;
        public bool IsChangesSaved => _isChangesSaved;

        public event Action? InfoChanged;

        public RepositoriesFacade TablesContext
        {
            get
            {
                if (_tablesContext == null)
                    _tablesContext = LoadTables();

                return _tablesContext;
            }
        }

        public Profile(string name)
        {
            _name = name;
        }

        public void SaveTables()
        {
            //LoadTables();
            //string profileFile = PathHelper.GetProfileFilePath(_name);
            TablesContext.SaveChanges();
        }

        private RepositoriesFacade LoadTables()
        {
            string connection = PathHelper.GetProfileDirectoryPath(_name);
            connection = Path.Combine(connection, "Info.db");
            connection = "Datasource=" + connection;

            var opt = SqliteDbContextOptionsBuilderExtensions.UseSqlite(new DbContextOptionsBuilder(), connection).Options;
            FilmsContext filmsContext = new FilmsContext(opt);

            string profileDirectory = PathHelper.GetProfileDirectoryPath(_name);
            string profileFile = PathHelper.GetProfileFilePath(_name);

            Directory.CreateDirectory(profileDirectory);

            if (File.Exists(profileFile) == false)
            {
                filmsContext.Database.Migrate();
                filmsContext.FilmGenres.Add(new FilmGenre { Id = 1, Name = "Movie", IsSerial = false });
                filmsContext.FilmGenres.Add(new FilmGenre { Id = 2, Name = "Series", IsSerial = true });

                filmsContext.BookGenres.Add(new BookGenre { Id = 1, Name = "Book" });
                filmsContext.SaveChanges();
            }
            else
            {
                filmsContext.Database.Migrate();
            }

            RepositoriesFacade repositories = new RepositoriesFacade(filmsContext);
            repositories.TablesSaved += OnTablesContextSaved;

            _isChangesSaved = true;
            ConfigureInfoChangedEvent(repositories);

            return repositories;
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

        private void OnTablesContextSaved(RepositoriesFacade sender)
        {
            _isChangesSaved = true;
        }
    }
}
