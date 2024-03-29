﻿using Filmc.Wpf.Helper;
using Filmc.Wpf.SaveConverters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Services
{
    public class ProfilesService
    {
        private Profile _profile;

        private readonly List<Profile> _profiles;

        public ProfilesService()
        {
            _profiles = new List<Profile>();
            LoadProfiles();

            _profile = _profiles.First();
        }

        public IEnumerable<Profile> Profiles => _profiles;

        public Profile SelectedProfile
        {
            get => _profile;
            set { _profile = value; SelectedProfileChanged?.Invoke(_profile); }
        }

        public event Action<Profile>? SelectedProfileChanged;
        public event Action<Profile>? ProfileRemoved;
        public event Action<Profile>? ProfileAdded;

        private void LoadProfiles()
        {
            DirectoryInfo profilesDir = Directory.CreateDirectory(PathHelper.ProfilesPath);
            DirectoryInfo[] directories = profilesDir.GetDirectories();

            if (directories.Length != 0)
            {
                foreach (var item in directories)
                {
                    _profiles.Add(new Profile(item.Name));
                    Converter.Convert(item.Name);
                }
            }
            else
            {
                _profiles.Add(new Profile("Main"));
            }
        }

        public Profile? CreateProfile(string profileName)
        {
            Profile? profile = null;

            if (_profiles.All(x => x.Name != profileName))
            {
                profile = new Profile(profileName);
                _profiles.Add(profile);

                ProfileAdded?.Invoke(profile);
            }

            return profile;
        }

        public bool RemoveProfile(Profile profile)
        {
            if (SelectedProfile != profile)
            {
                bool result = _profiles.Remove(profile);

                if (result)
                {
                    string root = PathHelper.GetProfileDirectoryPath(profile.Name);

                    if (Directory.Exists(root))
                        Directory.Delete(root, true);

                    ProfileRemoved?.Invoke(profile);
                }


                return result;
            }

            return false;
        }

        public void ImportProfile(string filePath)
        {
            int i = 0;
            string profName = "import";
            while (_profiles.Any(x => x.Name == profName + i) == false)
            {
                i++;
            }

            File.Copy(filePath, PathHelper.GetProfileFilePath(profName), true);
        }
    }
}
