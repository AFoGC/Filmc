﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Helper
{
    public static class PathHelper
    {
        public static readonly string MainDirectory;
        public static readonly string ProfilesPath;
        public static readonly string SettingsPath;
        public static readonly string ImagesResourcePath;
        public static readonly string UpdaterProgramPath;

        static PathHelper()
        {
            MainDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            SettingsPath = Path.Combine(MainDirectory, "ProgramSetting.xml");
            ProfilesPath = Path.Combine(MainDirectory, "Profiles");
            ImagesResourcePath = Path.Combine(MainDirectory, "Resources", "Background");
            UpdaterProgramPath = Path.Combine(MainDirectory, "updater", "FilmcUpdater.exe");
        }

        public static string GetProfileDirectoryPath(string profileName)
        {
            return Path.Combine(ProfilesPath, profileName);
        }

        public static string GetProfileFilePath(string profileName)
        {
            return Path.Combine(ProfilesPath, profileName, "Info.db");
        }

        public static string GetProfileSettingsPath(string profileName)
        {
            return Path.Combine(ProfilesPath, profileName, "Settings.xml");
        }

        public static SqliteConnection GetSqliteConnection(string filePath)
        {
            string connectionString = "Datasource=" + filePath;
            SqliteConnection connection = new SqliteConnection(connectionString);
            return connection;
        }

        public static void ClearSqlitePool(string filePath)
        {
            SqliteConnection connection = GetSqliteConnection(filePath);
            SqliteConnection.ClearPool(connection);
        }
    }
}
