using Filmc.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xtl;

namespace Filmc.Wpf.ViewModels
{
    public class StatusBarViewModel : BaseViewModel
    {
        private readonly ProfilesService _profilesService;
        private readonly Timer _backToNormalTimer;

        private Profile? _selectedProfile;

        private string _title;
        private StatusEnum _status;

        public StatusBarViewModel(ProfilesService profilesService)
        {
            _profilesService = profilesService;
            OnSelectedProfileChanged(_profilesService.SelectedProfile);
            _profilesService.SelectedProfileChanged += OnSelectedProfileChanged;

            _backToNormalTimer = new Timer();
            _backToNormalTimer.Interval = 2000;
            _backToNormalTimer.Elapsed += OnBackToNormalTimerTick;

            _title = string.Empty;
            _status = StatusEnum.Normal;
            
        }

        public StatusEnum Status
        {
            get => _status;
            private set
            { 
                _status = value;
                OnPropertyChanged(); 
            }
        }

        private void OnBackToNormalTimerTick(object? sender, ElapsedEventArgs e)
        {
            _backToNormalTimer.Stop();
            Status = StatusEnum.Normal;
        }

        private void OnSelectedProfileChanged(Profile profile)
        {
            if (_selectedProfile != null)
            {
                _selectedProfile.InfoChanged -= OnProfileInfoChanged;
                _selectedProfile.TablesContext.TablesSaved -= OnTablesSaved;
            }

            _selectedProfile = profile;

            _selectedProfile.InfoChanged += OnProfileInfoChanged;
            _selectedProfile.TablesContext.TablesSaved += OnTablesSaved;
        }

        private void OnTablesSaved(TablesCollection sender)
        {
            Status = StatusEnum.Saved;
            _backToNormalTimer.Start();
        }

        private void OnProfileInfoChanged()
        {
            Status = StatusEnum.UnSaved;
        }
    }
}
