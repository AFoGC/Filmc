using Filmc.Wpf.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Models
{
    public class ProfilesModel
    {
        private ProfileModel _profile;

        private readonly List<ProfileModel> _profiles;

        public ProfilesModel()
        {
            _profiles = new List<ProfileModel>();
            LoadProfiles();

            _profile = _profiles.First();
        }

        public IEnumerable<ProfileModel> Profiles => _profiles;

        public ProfileModel SelectedProfile
        {
            get => _profile;
            set { _profile = value; SelectedProfileChanged?.Invoke(_profile); }
        }

        public event Action<ProfileModel>? SelectedProfileChanged;
        public event Action<ProfileModel>? ProfileRemoved;
        public event Action<ProfileModel>? ProfileAdded;

        private void LoadProfiles()
        {
            DirectoryInfo profilesDir = Directory.CreateDirectory(PathHelper.ProfilesPath);
            DirectoryInfo[] directories = profilesDir.GetDirectories();

            if(directories.Length != 0)
            {
                foreach (var item in directories)
                {
                    _profiles.Add(new ProfileModel(item.Name));
                }
            }
            else
            {
                _profiles.Add(new ProfileModel("Main"));
            }
        }

        public ProfileModel? CreateProfile(string profileName)
        {
            ProfileModel? profile = null;

            if (_profiles.All(x => x.Name != profileName))
            {
                profile = new ProfileModel(profileName);
                _profiles.Add(profile);

                ProfileAdded?.Invoke(profile);
            }

            return profile;
        }
    }
}
