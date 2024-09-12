using Filmc.Wpf.Commands;
using Filmc.Wpf.SettingsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Filmc.Wpf.ViewModels
{
    public class BackgroundImageViewModel : BaseViewModel
    {
        private readonly BackgroundImageService _backgroundImageService;

        public BackgroundImageViewModel(BackgroundImageService backgroundImageService)
        {
            _backgroundImageService = backgroundImageService;
            _backgroundImageService.ImageChanged += OnImageChanged;
            _backgroundImageService.OpacityChanged += OnOpacityChanged;

            ChangeBackgroundImageCommand = new RelayCommand(ChangeBackgroundImage);
            RemoveBackgroundImageCommand = new RelayCommand(RemoveBackgroundImage);
        }

        public RelayCommand ChangeBackgroundImageCommand { get; }
        public RelayCommand RemoveBackgroundImageCommand { get; }

        public BitmapImage? Image => _backgroundImageService.Image;
        public string? ImageName => _backgroundImageService.ImageName;

        public double Opacity
        {
            get => _backgroundImageService.Opacity;
            set => _backgroundImageService.Opacity = value;
        }

        private void OnImageChanged(BitmapImage? image)
        {
            OnPropertyChanged(nameof(Image));
            OnPropertyChanged(nameof(ImageName));
        }

        private void OnOpacityChanged(double obj)
        {
            OnPropertyChanged(nameof(Opacity));
        }

        public void ChangeBackgroundImage(object? obj)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();

            //dialog.FileName = "Image";
            //dialog.DefaultExt = ".png";
            //dialog.Filter = "Images (.png)|*.png";

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                _backgroundImageService.SetNewImage(dialog.FileName);
            }
        }

        public void RemoveBackgroundImage(object? obj)
        {
            _backgroundImageService.SetNewImage(null);
        }
    }
}
