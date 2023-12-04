using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using FileMode = System.IO.FileMode;

namespace Filmc.Wpf.Updater.Module
{
    public static class Updater
    {
        public static async void UpdateFilmc()
        {
            var client = new GitHubClient(new ProductHeaderValue("Filmc"));
            var releases = client.Repository.Release.GetAll("AFoGC", "Filmc").Result;

            Release latest = releases[0];

            HttpClient httpClient = new HttpClient();
            Stream fileStream = await httpClient.GetStreamAsync(latest.ZipballUrl);

            using (FileStream outputFileStream = new FileStream("Filmc.zip", FileMode.Create))
            {
                fileStream.CopyTo(outputFileStream);
            }

            ZipFile.ExtractToDirectory("Filmc.zip", "");
        }

        public static bool IsFilmcLastVersion()
        {
            string prog = Assembly.GetExecutingAssembly().Location;
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(prog);
            string version = fileVersionInfo.ProductVersion;

            var client = new GitHubClient(new ProductHeaderValue("Filmc"));
            IReadOnlyList<Release> releases = client.Repository.Release.GetAll("AFoGC", "Filmc").Result;

            Release release = releases[0];

            return release.TagName == version;
        }

        public static void UpdateUpdater()
        {

        }
    }
}
