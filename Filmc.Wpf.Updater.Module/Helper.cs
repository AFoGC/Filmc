using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Updater.Module
{
    internal static class Helper
    {
        private static readonly HttpClient HttpClient;
        private static readonly GitHubClient GitHubClient;

        private static readonly string MainDirectory;
        private static readonly string UpdaterDirectory;
        private static readonly string UpdateTempPath;
        private static readonly string ZipFilePath;
        private static readonly string MainProgramPath;

        static Helper()
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "C# App");

            GitHubClient = new GitHubClient(new ProductHeaderValue("Filmc"));

            UpdaterDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            UpdateTempPath = Path.Combine(UpdaterDirectory, "update");
            ZipFilePath = Path.Combine(UpdateTempPath, "Filmc.zip");

            MainDirectory = Path.GetDirectoryName(UpdaterDirectory)!;
            //MainProgramPath = Path.Combine(MainDirectory, "Filmc.exe");
            MainProgramPath = Path.Combine(MainDirectory, "WpfApp.exe");
        }

        internal static IReadOnlyList<Release> GetReleases()
        {
            var releases = GitHubClient.Repository.Release.GetAll("AFoGC", "FilmsDBCWpf").Result;
            return releases;
        }

        internal static async Task DownloadLastRealise(IReadOnlyList<Release> releases)
        {
            Release latest = releases[0];
            string downloadUrl = latest.Assets[0].BrowserDownloadUrl;

            Directory.CreateDirectory(UpdateTempPath);
            
            byte[] fileBytes = await HttpClient.GetByteArrayAsync(downloadUrl);
            File.WriteAllBytes(ZipFilePath, fileBytes);

            ZipFile.ExtractToDirectory(ZipFilePath, UpdateTempPath);
        }

        internal static bool ReplaceFilmcFiles()
        {
            if (Directory.Exists(UpdateTempPath))
            {
                DirectoryInfo updateDirectory = new DirectoryInfo(UpdateTempPath);
                DirectoryInfo mainProgramDirectory = updateDirectory.GetDirectories().First();

                foreach (var file in mainProgramDirectory.GetFiles())
                {
                    string filePath = Path.Combine(MainDirectory, file.Name);
                    file.MoveTo(filePath, true);
                }

                foreach (var directory in mainProgramDirectory.GetDirectories())
                {
                    string directoryPath = Path.Combine(MainDirectory, directory.Name);

                    if (directoryPath != UpdaterDirectory)
                    {
                        if (Directory.Exists(directoryPath))
                            Directory.Delete(directoryPath);

                        directory.MoveTo(directoryPath);
                    }
                }

                return true;
            }

            return false;
        }

        internal static void RemoveUpdateFiles()
        {
            if (Directory.Exists(UpdateTempPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(UpdateTempPath);
                directoryInfo.Delete(true);
            }
        }

        internal static string GetProductVersion()
        {
            string prog = Assembly.GetExecutingAssembly().Location;
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(prog);
            return fileVersionInfo.ProductVersion!;
        }

        internal static void FilmcStartup()
        {
            Process.Start(MainProgramPath);
        }
    }
}
