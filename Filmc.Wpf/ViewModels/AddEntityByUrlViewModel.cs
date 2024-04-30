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
        private bool _isCancelButtonEnabled;
        private string _url;

        public AddEntityByUrlViewModel(AddEntityByUrlService addEntityByUrlService)
        {
            _isCloseButtonEnabled = true;
            _isCancelButtonEnabled = false;
            _url = String.Empty;

            AddEntityByUrlService = addEntityByUrlService;

            AddByUrlCommand = new AsyncRelayCommand(AddByUrl, ExeptionHandler);
            CancelRequestCommand = new RelayCommand(CancelRequest);
        }

        public ICommand AddByUrlCommand { get; }
        public ICommand CancelRequestCommand { get; }

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
        public bool IsCancelButtonEnabled
        {
            get => _isCancelButtonEnabled;
            set
            {
                _isCancelButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        protected void SetProcessStart()
        {
            IsCloseButtonEnabled = false;
            IsCancelButtonEnabled = true;
        }

        protected void SetProcessEnd()
        {
            IsCloseButtonEnabled = true;
            IsCancelButtonEnabled = false;
        }

        protected abstract Task AddByUrl();

        private void CancelRequest(object? obj)
        {
            AddEntityByUrlService.CancelRequest();
        }

        private void ExeptionHandler(Exception e)
        {

        }
    }
}
