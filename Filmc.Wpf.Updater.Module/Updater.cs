using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace Filmc.Wpf.Updater.Module
{
    public static class Updater
    {
        public static async Task<bool> UpdateFilmc()
        {
            bool exp = false;

            Helper.RemoveUpdateDir();

            var releases = Helper.GetReleases();
            await Helper.DownloadLastRealise(releases);

            exp = Helper.ReplaceFilmcFiles();
            Helper.RemoveUpdateDir();

            return exp;
        }

        public static bool IsFilmcLastVersion()
        {
            string version = Helper.GetProductVersion();

            IReadOnlyList<Release> releases = Helper.GetReleases();
            Release release = releases[0];

            return release.TagName == version;
        }

        public static void IsUpdaterLastVersion()
        {
            throw new NotImplementedException();
        }

        public static void UpdateUpdater()
        {
            throw new NotImplementedException();
        }
    }
}
