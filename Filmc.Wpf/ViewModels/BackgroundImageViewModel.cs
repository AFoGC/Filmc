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

        private RelayCommand? changeBackgroundImageCommand;
        private RelayCommand? removeBackgroundImageCommand;

        public BackgroundImageViewModel(BackgroundImageService backgroundImageService)
        {
            _backgroundImageService = backgroundImageService;
            _backgroundImageService.ImageChanged += OnImageChanged;
            _backgroundImageService.OpacityChanged += OnOpacityChanged;
        }

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

        public RelayCommand ChangeBackgroundImageCommand
        {
            get
            {
                return changeBackgroundImageCommand ??
                (changeBackgroundImageCommand = new RelayCommand(obj =>
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
                }));
            }
        }

        public RelayCommand RemoveBackgroundImageCommand
        {
            get
            {
                return removeBackgroundImageCommand ??
                (removeBackgroundImageCommand = new RelayCommand(obj =>
                {
                    _backgroundImageService.SetNewImage(null);
                }));
            }
        }
    }
}
