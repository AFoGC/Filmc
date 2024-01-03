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

            Helper.RemoveUpdateFiles();

            var releases = Helper.GetReleases();
            await Helper.DownloadLastRealise(releases);

            exp = Helper.ReplaceFilmcFiles();

            return exp;
        }

        public static bool UpdateUpdater()
        {
            bool exp = false;

            Helper.ReplaceUpdaterFiles();
            Helper.RemoveUpdateFiles();

            return exp;
        }

        public static UpdateInfo? GetLastUpdate(Assembly assembly)
        {
            string version = Helper.GetProductVersion(assembly);

            IReadOnlyList<Release> releases = Helper.GetReleases();
            Release release = releases[0];

            if (release.TagName != version)
            {
                return new UpdateInfo
                {
                    Tag = release.TagName,
                    Description = release.Body
                };
            }
            else
            {
                return null;
            }
        }

        public static void RunFilmcAfterUpdate()
        {
            Helper.FilmcStartup("-uu");
        }
    }
}
