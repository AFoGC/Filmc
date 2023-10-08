using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.Models
{
    public class ProfilesModel
    {
        private ProfileModel _profile;

        public ProfilesModel()
        {
            Profiles = new List<ProfileModel>();
            _profile = null;
        }

        public List<ProfileModel> Profiles { get; }
        public ProfileModel SelectedProfile
        {
            get => _profile;
            set { _profile = value; SelectedProfileChanged?.Invoke(_profile); }
        }

        public event Action<ProfileModel>? SelectedProfileChanged;
    }
}
