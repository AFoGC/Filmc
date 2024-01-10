using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Updater.Module
{
    public class ProgramsUpdater
    {
        private readonly HttpClient _httpClient;
        private readonly GitHubClient _gitHubClient;

        private readonly string _mainDirectory;
        private readonly string _updaterDirectory;
        private readonly string _profilesDirectory;
        private readonly string _mainProgramPath;

        private readonly string _updateTempPath;
        private readonly string _zipFilePath;

        private readonly string[] _exclusiveDirectories;
        
        public ProgramsUpdater(string mainDirectory)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "C# App");

            _gitHubClient = new GitHubClient(new ProductHeaderValue("Filmc"));

            _mainDirectory = mainDirectory;
            _updaterDirectory = Path.Combine(_mainDirectory, "updater");
            _profilesDirectory = Path.Combine(_mainDirectory, "Profiles");
            _mainProgramPath = Path.Combine(_mainDirectory, "Filmc.exe");

            _updateTempPath = Path.Combine(_updaterDirectory, "update");
            _zipFilePath = Path.Combine(_updateTempPath, "Filmc.zip");

            _exclusiveDirectories = new string[] { _profilesDirectory, _updaterDirectory };
        }

        public async Task<bool> UpdateFilmc()
        {
            bool exp = false;

            RemoveUpdateFiles();

            var release = GetLatestRelease();

            if (release != null)
            {
                await DownloadLastRealise(release);
                exp = ReplaceFilmcFiles();
            }

            return exp;
        }

        public bool UpdateUpdater()
        {
            bool exp = false;

            exp = ReplaceUpdaterFiles();
            RemoveUpdateFiles();

            return exp;
        }

        public UpdateInfo? GetLastUpdate()
        {
            string version = GetProductVersion();
            Release release = GetLatestRelease();

            if (release != null)
            {
                if (release.TagName != version)
                {
                    return new UpdateInfo
                    {
                        Tag = release.TagName,
                        Description = release.Body
                    };
                }
            }

            return null;
        }

        public void FilmcStartup()
        {
            FilmcStartup("-uu");
        }

        private void FilmcStartup(string arguments)
        {
            Process.Start(_mainProgramPath, arguments);
        }

        private Release? GetLatestRelease()
        {
            Release? release;
            try
            {
                release = _gitHubClient.Repository.Release.GetLatest("AFoGC", "Filmc").Result;
            }
            catch (AggregateException)
            {
                release = null;
            }

            return release;
        }

        private IReadOnlyList<Release> GetReleases()
        {
            var releases = _gitHubClient.Repository.Release.GetAll("AFoGC", "Filmc").Result;
            return releases;
        }

        private async Task DownloadLastRealise(Release latest)
        {
            string downloadUrl = latest.Assets[0].BrowserDownloadUrl;

            Directory.CreateDirectory(_updateTempPath);

            byte[] fileBytes = await _httpClient.GetByteArrayAsync(downloadUrl);
            File.WriteAllBytes(_zipFilePath, fileBytes);

            ZipFile.ExtractToDirectory(_zipFilePath, _updateTempPath);
        }

        private bool ReplaceFilmcFiles()
        {
            if (Directory.Exists(_updateTempPath))
            {
                DirectoryInfo updateDirectory = new DirectoryInfo(_updateTempPath);
                DirectoryInfo mainProgramDirectory = updateDirectory.GetDirectories().First();

                foreach (var file in mainProgramDirectory.GetFiles())
                {
                    string filePath = Path.Combine(_mainDirectory, file.Name);
                    file.MoveTo(filePath, true);
                }

                foreach (var directory in mainProgramDirectory.GetDirectories())
                {
                    string directoryPath = Path.Combine(_mainDirectory, directory.Name);

                    if (_exclusiveDirectories.Contains(directoryPath) == false)
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

        private bool ReplaceUpdaterFiles()
        {
            if (Directory.Exists(_updateTempPath))
            {
                DirectoryInfo updateDirectory = new DirectoryInfo(_updateTempPath);
                DirectoryInfo updaterDirectory = updateDirectory
                    .GetDirectories()
                    .First()
                    .GetDirectories()
                    .First(x => x.Name == "updater");

                //updaterDirectory.MoveTo(_updaterDirectory);

                foreach (var file in updaterDirectory.GetFiles())
                {
                    file.MoveTo(Path.Combine(_updaterDirectory, file.Name), true);
                }
                

                return true;
            }

            return false;
        }

        private void RemoveUpdateFiles()
        {
            if (Directory.Exists(_updateTempPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(_updateTempPath);
                directoryInfo.Delete(true);
            }
        }

        private string GetProductVersion()
        {
            string filePath = _mainProgramPath;
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);
            return fileVersionInfo.ProductVersion!;
        }
    }
}
