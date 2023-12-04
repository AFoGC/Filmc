using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FileMode = System.IO.FileMode;

namespace Filmc.Wpf.Updater.Module
{
    internal static class Helper
    {
        private static string MainDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        private static string UpdatePath => Path.Combine(MainDirectory, "update");

        internal static IReadOnlyList<Release> GetReleases()
        {
            var client = new GitHubClient(new ProductHeaderValue("Filmc"));
            var releases = client.Repository.Release.GetAll("AFoGC", "FilmsDBCWpf").Result;
            return releases;
        }

        internal static async Task DownloadLastRealise(IReadOnlyList<Release> releases)
        {
            Release latest = releases[0];

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "C# App");

            string zipPath = Path.Combine(MainDirectory, UpdatePath, "Filmc.zip");

            string downloadUrl = latest.Assets[0].BrowserDownloadUrl;
            Stream fileStream = await httpClient.GetStreamAsync(downloadUrl);

            using (FileStream outputFileStream = new FileStream(zipPath, FileMode.Create))
            {
                fileStream.CopyTo(outputFileStream);
            }

            ZipFile.ExtractToDirectory(zipPath, UpdatePath);
        }

        internal static bool ReplaceFilmcFiles()
        {
            if (Directory.Exists(UpdatePath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(UpdatePath);
                FileInfo[] files = directoryInfo.GetFiles();

                foreach (var file in files)
                {
                    string filePath = Path.Combine(MainDirectory, file.Name);
                    file.MoveTo(filePath, true);
                }

                return false;
            }

            return true;
        }

        internal static void RemoveUpdateDir()
        {
            if (Directory.Exists(UpdatePath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(UpdatePath);
                directoryInfo.Delete();
            }
        }

        internal static string GetProductVersion()
        {
            string prog = Assembly.GetExecutingAssembly().Location;
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(prog);
            return fileVersionInfo.ProductVersion!;
        }
    }
}
