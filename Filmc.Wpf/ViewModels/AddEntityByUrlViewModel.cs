using Filmc.SitesIntegration;
using Filmc.Wpf.Commands;
using Filmc.Wpf.Services;
using System;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Filmc.Wpf.ViewModels
{
    public abstract class AddEntityByUrlViewModel : BaseViewModel
    {
        protected readonly AddEntityByUrlService AddEntityByUrlService;

        private bool _isCloseButtonEnabled;
        private string _url;

        public AddEntityByUrlViewModel(AddEntityByUrlService addEntityByUrlService)
        {
            _isCloseButtonEnabled = true;
            _url = String.Empty;

            AddEntityByUrlService = addEntityByUrlService;

            AddByUrlCommand = new AsyncRelayCommand(AddByUrl, ExeptionHandler);
        }

        public ICommand AddByUrlCommand { get; }

        public string Url
        { 
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }
        public bool IsCloseButtonEnabled
        {
            get => _isCloseButtonEnabled;
            set
            {
                _isCloseButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        protected abstract Task AddByUrl();

        private void ExeptionHandler(Exception e)
        {

        }
    }
}
