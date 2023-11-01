using Filmc.Wpf.Services;
using Filmc.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmc.Wpf.EntityViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public Profile Profile { get; }

        private bool _isSelected;

        public ProfileViewModel(Profile profile)
        {
            Profile = profile;
            _isSelected = false;
        }

        public string Name
        {
            get => Profile.Name;
        }

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }
    }
}
